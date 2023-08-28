using System.Diagnostics;

using Microsoft.Extensions.Options;

namespace Chart.Core.Instrumentation
{
    /// <summary>
    /// Middleware for measuring timings and reporting diagnostics in the response.
    /// </summary>
    public class MetricsServerEventListener : ServerEventListener
    {
        private readonly Stopwatch _stopwatch = new();

        private readonly IOptions<GraphOptions> _options;
        private readonly MetricsExtensionModel _metricsModel;

        public MetricsServerEventListener(
            IOptions<GraphOptions> options,
            IMetricsExtensionModelAccessor metricsModelAccessor)
        {
            this._options = options;
            this._metricsModel = metricsModelAccessor.Model;
        }

        public override void RequestReceived(QueryExecutionContext context)
        {
            if(!this._options.Value.Request.EnableMetrics)
            {
                return;
            }

            context.Result.Extensions.Add("tracing", new Dictionary<string, object>());

            this._metricsModel.StartTime = DateTime.UtcNow;
            this._stopwatch.Start();
        }

        public override void RequestEnded(QueryExecutionContext context)
        {
            if(!this._options.Value.Request.EnableMetrics)
            {
                return;
            }

            this._stopwatch.Stop();
            this._metricsModel.EndTime = DateTime.UtcNow;
            this._metricsModel.Duration = this._stopwatch.Elapsed.TotalMilliseconds;

            context.Result.Extensions["tracing"] = this._metricsModel;
        }
    }
}