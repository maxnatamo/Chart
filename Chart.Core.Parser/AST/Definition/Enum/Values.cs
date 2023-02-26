namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of an enum in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#EnumTypeDefinition">Original documentation</seealso>
    public class GraphEnumDefinitionValues : GraphNode
    {
        /// <summary>
        /// List of nodes in the enum.
        /// </summary>
        public List<GraphEnumDefinitionValue> Values { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphEnumDefinitionValues]";
        }
    }
}