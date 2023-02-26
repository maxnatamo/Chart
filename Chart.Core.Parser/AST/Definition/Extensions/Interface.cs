namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a scalar extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ScalarTypeExtension">Original documentation</seealso>
    public class GraphInterfaceExtension : GraphExtensionDefinition
    {
        /// <summary>
        /// The type of extension in the document.
        /// </summary>
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Interface;

        /// <summary>
        /// Optional interface of the interface.
        /// </summary>
        public GraphInterfaces? Interface { get; set; } = null;

        /// <summary>
        /// Optional fields for the interface.
        /// </summary>
        public GraphFields? Fields { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphInterfaceExtension]";
        }
    }
}