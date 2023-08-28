using Chart.Core.Instrumentation;

using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core.Execution
{
    public static partial class RequestServiceExtensions
    {
        /// <summary>
        /// Add the default execution pipeline and all of it's dependencies to the given service container.
        /// </summary>
        public static RequestServiceBuilder AddPipelineExecution(this RequestServiceBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<IOperationResolver, DefaultOperationResolver>();
            builder.Services.AddScoped<IExecutionStrategySelector, DefaultExecutionStrategySelector>();

            builder.AddDefaultExecutionPipeline();

            return builder;
        }

        /// <summary>
        /// Register the default execution pipeline.
        /// </summary>
        private static RequestServiceBuilder AddDefaultExecutionPipeline(this RequestServiceBuilder builder) => builder
            .SetExecutionPipeline<DefaultExecutionPipeline>()
            .AddMetricsInstrumentation()
            .AddDiagnostics()
            .AddDefaultExecutionServices();

        /// <summary>
        /// Register the default execution pipeline.
        /// </summary>
        private static RequestServiceBuilder AddDefaultExecutionServices(this RequestServiceBuilder builder) => builder
            .AddRequestExecutionMiddleware<ExceptionHandlingMiddleware>()
            .AddRequestExecutionMiddleware<DocumentParsingMiddleware>()
            .AddRequestExecutionMiddleware<ValidationMiddleware>()
            .AddRequestExecutionMiddleware<OperationResolverMiddleware>()
            .AddRequestExecutionMiddleware<OperationCollectionMiddleware>()
            .AddRequestExecutionMiddleware<ExecutionMiddleware>()
            .AddFieldExecutionMiddleware<FieldExecutionMiddleware>();

        /// <summary>
        /// Replace the current execution pipeline with a new execution pipeline.
        /// </summary>
        public static RequestServiceBuilder SetExecutionPipeline<TPipeline>(
            this RequestServiceBuilder builder)
            where TPipeline : class, IExecutionPipeline
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<IExecutionPipeline, TPipeline>();
            return builder;
        }

        /// <summary>
        /// Add a new request execution middleware to the current execution pipeline.
        /// </summary>
        /// <remarks>
        /// The middleware is added in the same order that they're appended to the dependency injection container.
        /// </remarks>
        internal static RequestServiceBuilder AddRequestExecutionMiddleware<TMiddleware>(
            this RequestServiceBuilder builder)
            where TMiddleware : class, IRequestExecutionMiddleware
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<IRequestExecutionMiddleware, TMiddleware>();
            return builder;
        }

        /// <summary>
        /// Add a new field execution middleware to the current execution pipeline.
        /// </summary>
        /// <remarks>
        /// The middleware is added in the same order that they're appended to the dependency injection container.
        /// </remarks>
        internal static RequestServiceBuilder AddFieldExecutionMiddleware<TMiddleware>(
            this RequestServiceBuilder builder)
            where TMiddleware : class, IFieldExecutionMiddleware
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.Services.AddScoped<IFieldExecutionMiddleware, TMiddleware>();
            return builder;
        }
    }
}