namespace Chart.Core
{
    public static partial class RequestServiceBuilderExtensions
    {
        /// <inheritdoc cref="AddType{TType}(RequestServiceBuilder, string?)" />
        /// <typeparam name="TType">
        /// <inheritdoc cref="AddType" path="/param[@name='type']" />
        /// </typeparam>
        public static RequestServiceBuilder AddType<TType>(
            this RequestServiceBuilder builder,
            string? typeName = null)
            => builder.AddType(typeof(TType), typeName);

        /// <summary>
        /// Add the given type to the schema builder.
        /// </summary>
        /// <param name="type">The type to add to the schema builder.</param>
        /// <param name="typeName">An explicit name for the type. If not defined, the name is inferred from the type.</param>
        /// <returns>The current instance, allowing for method chaining.</returns>
        public static RequestServiceBuilder AddType(
            this RequestServiceBuilder builder,
            Type type,
            string? typeName = null)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));
            builder.SchemaBuilder.AddType(type, typeName);

            return builder;
        }
    }
}