namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of an object extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ObjectTypeExtension">Original documentation</seealso>
    public class GraphObjectExtension : GraphExtensionDefinition
    {
        /// <summary>
        /// The type of extension in the document.
        /// </summary>
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Object;

        /// <summary>
        /// Optional interface of the interface.
        /// </summary>
        public GraphInterfaces? Interface { get; set; } = null;

        /// <summary>
        /// Underlying fields of the type definition.
        /// </summary>
        public GraphFields? Fields { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphObjectExtension]";
        }
    }
}