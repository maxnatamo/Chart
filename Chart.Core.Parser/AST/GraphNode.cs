namespace Chart.Core.Parser
{
    /// <summary>
    /// Abstract class of all nodes in the parsed GraphQL-tree.
    /// </summary>
    public abstract class GraphNode
    {
        /// <summary>
        /// The location of the ASTNode.
        /// </summary>
        public GraphLocation Location { get; set; } = default!;
    }
}