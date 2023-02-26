namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of an enum value in an enum definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#EnumValueDefinition">Original documentation</seealso>
    public class GraphEnumDefinitionValue : GraphNode
    {
        /// <summary>
        /// List of nodes in the enum.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// The optional description for the enum value.
        /// </summary>
        public GraphDescription? Description { get; set; } = null;

        /// <summary>
        /// Optional directives of the enum.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphEnumDefinitionValue]";
        }
    }
}