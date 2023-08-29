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
        public GraphDirectiveLocationFlags Locations { get; set; }

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphDirectiveLocations]";
    }
}