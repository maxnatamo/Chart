namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public class GraphDirectiveLocations : GraphNode
    {
        /// <summary>
        /// List of directive locations selected by the directive.
        /// </summary>
        public List<GraphName> Locations { get; set; } = new List<GraphName>();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphDirectiveLocations]";
        }
    }
}