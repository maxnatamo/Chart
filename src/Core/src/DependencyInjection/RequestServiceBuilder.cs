using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public class RequestServiceBuilder
    {
        public readonly IServiceCollection Services;

        internal readonly SchemaBuilder SchemaBuilder;

        internal RequestServiceBuilder()
            : this(new ServiceCollection())
        { }

        internal RequestServiceBuilder(IServiceCollection services)
        {
            this.Services = services;
            this.SchemaBuilder = new(this);

            this.ResetOptions();
        }

        /// <summary>
        /// Reset the current options of the builder and set the new options.
        /// </summary>
        /// <param name="configure">Action for configuring the builder.</param>
        /// <returns>The same instance, allowing for method chaining.</returns>
        public RequestServiceBuilder SetOptions(Action<GraphOptions> configure)
        {
            this.Services.Configure<GraphOptions>(opts =>
            {
                opts = new GraphOptions();
                configure(opts);
            });
            return this;
        }

        /// <summary>
        /// Modify the current options of the builder, using <paramref name="configure" />.
        /// </summary>
        /// <param name="configure">Action for configuring the builder.</param>
        /// <returns>The same instance, allowing for method chaining.</returns>
        public RequestServiceBuilder ModifyOptions(Action<GraphOptions> configure)
        {
            this.Services.Configure(configure);
            return this;
        }

        /// <summary>
        /// Reset the options of the builder to the default.
        /// </summary>
        /// <returns>The same instance, allowing for method chaining.</returns>
        public RequestServiceBuilder ResetOptions()
            => this.SetOptions(_ => { });

        /// <inheritdoc cref="ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(IServiceCollection)" />
        public IServiceProvider BuildServiceProvider()
        {
            IServiceProvider serviceProvider = this.Services.BuildServiceProvider();

            Schema schema = this.BuildSchema(serviceProvider);
            serviceProvider.UpdateSchema(schema);

            return serviceProvider;
        }

        /// <summary>
        /// Build the first iteration of the schema.
        /// </summary>
        /// <param name="serviceProvider">
        /// Service provider corresponding to the built version of <see cref="RequestServiceBuilder.Services" />.
        /// </param>
        /// <returns>The built schema.</returns>
        private Schema BuildSchema(IServiceProvider serviceProvider)
            => this.SchemaBuilder.Build(serviceProvider);
    }
}