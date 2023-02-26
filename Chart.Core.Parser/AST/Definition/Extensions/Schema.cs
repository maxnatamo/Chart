namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a scema extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#SchemaExtension">Original documentation</seealso>
    public class GraphSchemaExtension : GraphExtensionDefinition
    {
        /// <summary>
        /// The type of extension in the document.
        /// </summary>
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Schema;

        /// <summary>
        /// Values in the schema.
        /// </summary>
        public GraphSchemaValues Values { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphSchemaExtension]";
        }
    }
}