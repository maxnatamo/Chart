using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Instrumentation
{
    public static partial class RequestServiceExtensions
    {
        public static RequestServiceBuilder AddDiagnostics(this RequestServiceBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<IExecutionEventRaiser, ExecutionEventRaiser>();
            builder.Services.AddScoped<IServerEventRaiser, ServerEventRaiser>();

            return builder;
        }

        /// <summary>
        /// Add a new execution event listener to the container.
        /// </summary>
        public static RequestServiceBuilder AddExecutionEventListener<TListener>(this RequestServiceBuilder builder)
            where TListener : ExecutionEventListener
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<ExecutionEventListener, TListener>();
            return builder;
        }

        /// <summary>
        /// Add a new server event listener to the container.
        /// </summary>
        public static RequestServiceBuilder AddServerEventListener<TListener>(this RequestServiceBuilder builder)
            where TListener : ServerEventListener
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<ServerEventListener, TListener>();
            return builder;
        }
    }
}