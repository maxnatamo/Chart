namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an enum in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#EnumTypeDefinition">Original documentation</seealso>
    public class GraphEnumDefinitionValues : IGraphNode
    {
        /// <summary>
        /// List of nodes in the enum.
        /// </summary>
        public List<GraphEnumDefinitionValue> Values { get; set; } = default!;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphEnumDefinitionValues]";
    }
}