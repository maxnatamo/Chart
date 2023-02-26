namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of an enum extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#EnumTypeExtension">Original documentation</seealso>
    public class GraphEnumExtension : GraphExtensionDefinition
    {
        /// <summary>
        /// The type of extension in the document.
        /// </summary>
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Enum;

        /// <summary>
        /// List of values in the enum.
        /// </summary>
        public GraphEnumDefinitionValues? Values { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphEnumExtension]";
        }
    }
}