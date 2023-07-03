namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public class GraphDirectiveLocations : IGraphNode
    {
        /// <summary>
        /// List of directive locations selected by the directive.
        /// </summary>
        public List<GraphName> Locations { get; set; } = new List<GraphName>();

        /// <inheritdoc />
        public GraphLocation? Location { get; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphDirectiveLocations]";
    }
}