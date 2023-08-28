namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a union member a union definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#UnionMemberTypes">Original documentation</seealso>
    public class GraphUnionMembers : IGraphNode
    {
        /// <summary>
        /// List of members in the union
        /// </summary>
        public List<GraphName> Members { get; set; } = new List<GraphName>();

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphUnionMembers]";
    }
}