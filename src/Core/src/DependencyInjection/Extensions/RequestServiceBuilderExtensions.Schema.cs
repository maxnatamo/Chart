namespace Chart.Core
{
    public static partial class RequestServiceBuilderExtensions
    {
        /// <summary>
        /// Add a schema string to the builder, allowing types to be bound to it.
        /// </summary>
        /// <param name="schema">The schema SDL string to use as a schema base.</param>
        /// <returns>The current instance, allowing for method chaining.</returns>
        public static RequestServiceBuilder AddSchema(
            this RequestServiceBuilder builder,
            string schema)
        {
            if(string.IsNullOrEmpty(schema))
            {
                return builder;
            }

            builder.SchemaBuilder.FromSchema(schema);
            return builder;
        }

        /// <summary>
        /// Add a configuration step before creating the final schema.
        /// </summary>
        /// <param name="configure">The configuration step to add.</param>
        /// <returns>The current instance, allowing for method chaining.</returns>
        public static RequestServiceBuilder ConfigureSchema(
            this RequestServiceBuilder builder,
            SchemaConfigurationDelegate configure)
        {
            builder.SchemaBuilder.Configure(configure);
            return builder;
        }
    }
}