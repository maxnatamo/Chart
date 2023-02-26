namespace Chart.Core.Parser
{
    /// <summary>
    /// Top-level node of the GraphQL-document.
    /// </summary>
    public class GraphDocument : GraphNode
    {
        /// <summary>
        /// List of underlying node definitions in the document.
        /// </summary>
        public List<GraphDefinition> Definitions { get; set; } = new List<GraphDefinition>();

        /// <summary>
        /// The source string of the original GraphQL-document.
        /// </summary>
        public string Source { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphDocument]";
        }
    }
}