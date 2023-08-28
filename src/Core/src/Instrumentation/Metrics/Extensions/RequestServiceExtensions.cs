using Chart.Core.Execution;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Instrumentation
{
    public static partial class RequestServiceExtensions
    {
        public static RequestServiceBuilder AddMetricsInstrumentation(this RequestServiceBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<IMetricsExtensionModelAccessor, MetricsExtensionModelAccessor>();

            builder.AddServerEventListener<MetricsServerEventListener>();
            builder.AddExecutionEventListener<MetricsExecutionEventListener>();
            builder.AddFieldExecutionMiddleware<FieldMetricsMiddleware>();

            return builder;
        }
    }
}