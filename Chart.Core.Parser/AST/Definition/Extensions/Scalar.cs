namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a scalar extension in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ScalarTypeExtension">Original documentation</seealso>
    public class GraphScalarExtension : GraphExtensionDefinition
    {
        /// <summary>
        /// The type of extension in the document.
        /// </summary>
        public override GraphExtensionKind ExtensionKind => GraphExtensionKind.Scalar;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphScalarExtension]";
        }
    }
}