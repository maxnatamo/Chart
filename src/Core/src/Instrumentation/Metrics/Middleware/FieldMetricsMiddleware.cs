using System.Diagnostics;

using Microsoft.Extensions.Options;

namespace Chart.Core.Instrumentation
{
    /// <summary>
    /// Middleware for measuring timings and reporting diagnostics in the response.
    /// </summary>
    public class FieldMetricsMiddleware : IFieldExecutionMiddleware
    {
        private readonly Stopwatch _stopwatch = new();

        private readonly IOptions<GraphOptions> _options;
        private readonly MetricsExtensionModel _metricsModel;

        public FieldMetricsMiddleware(
            IOptions<GraphOptions> options,
            IMetricsExtensionModelAccessor metricsModelAccessor)
        {
            this._options = options;
            this._metricsModel = metricsModelAccessor.Model;
        }

        /// <inheritdoc />
        public ValueTask<object?> InvokeAsync(
            ResolverContext context,
            FieldExecutionDelegate next,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return this._options.Value.Request.EnableMetrics
                ? this.MeasureMetricsAsync(context, next, cancellationToken)
                : next(context, cancellationToken);
        }

        private async ValueTask<object?> MeasureMetricsAsync(
            ResolverContext context,
            FieldExecutionDelegate next,
            CancellationToken cancellationToken)
        {
            MetricsFieldExtensionModel metrics = new()
            {
                ParentType = context.ObjectType.Name,
                FieldName = context.Selection.Name,
                ReturnType = context.Field.Type.ToString(),

                StartOffset = (DateTime.UtcNow - this._metricsModel.StartTime).TotalMilliseconds
            };

            this._stopwatch.Start();
            object? result = await next(context, cancellationToken);
            this._stopwatch.Stop();

            metrics.Duration = this._stopwatch.Elapsed.TotalMilliseconds;
            this._metricsModel.Execution.Resolvers.Add(metrics);

            return result;
        }
    }
}