namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of interface implementations in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ImplementsInterfaces">Original documentation</seealso>
    public class GraphInterfaces : IGraphNode
    {
        /// <summary>
        /// The underlying list of implemented types.
        /// </summary>
        public List<GraphNamedType> Implements { get; set; } = new List<GraphNamedType>();

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphInterfaces]";
    }
}