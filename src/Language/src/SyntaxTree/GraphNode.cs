namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Interface of all nodes in the parsed GraphQL-tree.
    /// </summary>
    public interface IGraphNode
    {
        /// <summary>
        /// The location of the ASTNode.
        /// </summary>
        public GraphLocation? Location { get; set; }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public string ToString();
    }
}