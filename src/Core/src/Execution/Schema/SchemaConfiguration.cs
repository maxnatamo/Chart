namespace Chart.Core
{
    public delegate void SchemaConfigurationDelegate(SchemaBuilder builder, IServiceProvider services);

    /// <summary>
    /// A single configuration step for the <see cref="Schema" />, before it is created.
    /// </summary>
    public interface ISchemaConfiguration
    {
        /// <summary>
        /// Executes the current instance's configuration and appends it to the schema.
        /// </summary>
        /// <param name="schemaBuilder">
        /// The <see cref="SchemaBuilder" />-instance, which will create the final <see cref="Schema"/>-instance.
        /// </param>
        /// <param name="provider">The service provider to get services from.</param>
        void Configure(SchemaBuilder schemaBuilder, IServiceProvider provider);
    }

    public sealed class SchemaConfiguration : ISchemaConfiguration
    {
        private readonly SchemaConfigurationDelegate _configure;

        public SchemaConfiguration(SchemaConfigurationDelegate configure)
            => this._configure = configure ?? throw new ArgumentNullException(nameof(configure));

        public void Configure(SchemaBuilder schemaBuilder, IServiceProvider provider)
            => this._configure(schemaBuilder, provider);
    }
}