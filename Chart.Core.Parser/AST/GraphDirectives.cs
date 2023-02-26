namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of directives in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Directives">Original documentation</seealso>
    public class GraphDirectives : GraphNode
    {
        /// <summary>
        /// The name for the directive.
        /// </summary>
        public List<GraphDirective> Directives { get; set; } = new List<GraphDirective>();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphDirectives]";
        }
    }
}