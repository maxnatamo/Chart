namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Top-level node of the GraphQL-document.
    /// </summary>
    public class GraphDocument : IGraphNode
    {
        /// <summary>
        /// List of underlying node definitions in the document.
        /// </summary>
        public List<GraphDefinition> Definitions { get; set; } = new List<GraphDefinition>();

        /// <summary>
        /// The source string of the original GraphQL-document.
        /// </summary>
        public string Source { get; set; } = default!;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphDocument]";
    }
}