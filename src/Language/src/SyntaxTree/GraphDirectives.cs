namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of directives in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Directives">Original documentation</seealso>
    public class GraphDirectives : IGraphNode
    {
        /// <summary>
        /// The name for the directive.
        /// </summary>
        public List<GraphDirective> Directives { get; set; } = new List<GraphDirective>();

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphDirectives]";
    }
}