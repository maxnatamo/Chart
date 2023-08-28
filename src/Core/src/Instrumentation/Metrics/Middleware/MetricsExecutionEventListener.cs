using System.Diagnostics;

using Microsoft.Extensions.Options;

namespace Chart.Core.Instrumentation
{
    /// <summary>
    /// Middleware for measuring timings and reporting diagnostics in the response.
    /// </summary>
    public class MetricsExecutionEventListener : ExecutionEventListener
    {
        private readonly Stopwatch _stopwatch = new();

        private readonly IOptions<GraphOptions> _options;
        private readonly MetricsExtensionModel _metricsModel;

        public MetricsExecutionEventListener(
            IOptions<GraphOptions> options,
            IMetricsExtensionModelAccessor metricsModelAccessor)
        {
            this._options = options;
            this._metricsModel = metricsModelAccessor.Model;
        }

        /// <inheritdoc />
        public override void ParseDocument(QueryExecutionContext context)
        {
            if(!this._options.Value.Request.EnableMetrics)
            {
                return;
            }

            this._stopwatch.Start();
            this._metricsModel.Parsing.StartOffset = (DateTime.UtcNow - this._metricsModel.StartTime).TotalMilliseconds;
        }

        public override void ParseDocumentFinished(QueryExecutionContext context)
        {
            if(!this._options.Value.Request.EnableMetrics)
            {
                return;
            }

            this._stopwatch.Stop();
            this._metricsModel.Parsing.Duration = this._stopwatch.Elapsed.TotalMilliseconds;
        }

        /// <inheritdoc />
        public override void ValidateDocument(QueryExecutionContext context)
        {
            if(!this._options.Value.Request.EnableMetrics)
            {
                return;
            }

            this._stopwatch.Restart();
            this._metricsModel.Validation.StartOffset = (DateTime.UtcNow - this._metricsModel.StartTime).TotalMilliseconds;
        }

        public override void ValidateDocumentFinished(QueryExecutionContext context)
        {
            if(!this._options.Value.Request.EnableMetrics)
            {
                return;
            }

            this._stopwatch.Stop();
            this._metricsModel.Validation.Duration += this._stopwatch.Elapsed.TotalMilliseconds;
        }

        /// <inheritdoc />
        public override void DocumentExecution(ResolverContext context)
        {
            if(!this._options.Value.Request.EnableMetrics)
            {
                return;
            }

            this._stopwatch.Restart();
            this._metricsModel.Execution.StartOffset = (DateTime.UtcNow - this._metricsModel.StartTime).TotalMilliseconds;
        }

        public override void DocumentExecutionFinished(ResolverContext context)
        {
            if(!this._options.Value.Request.EnableMetrics)
            {
                return;
            }

            this._stopwatch.Stop();
            this._metricsModel.Execution.Duration = this._stopwatch.Elapsed.TotalMilliseconds;
        }
    }
}