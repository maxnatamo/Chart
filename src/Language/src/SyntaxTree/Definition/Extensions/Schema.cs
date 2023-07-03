namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a scema extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#SchemaExtension">Original documentation</seealso>
    public class GraphSchemaExtension : GraphExtensionDefinition
    {
        /// <inheritdoc />
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Schema;

        /// <summary>
        /// Values in the schema.
        /// </summary>
        public GraphSchemaValues Values { get; set; } = default!;

        /// <inheritdoc />
        public override string ToString() => "[GraphSchemaExtension]";
    }
}