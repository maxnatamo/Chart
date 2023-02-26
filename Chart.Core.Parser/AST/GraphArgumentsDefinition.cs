namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a list of arguments in a type definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ArgumentsDefinition">Original documentation</seealso>
    public class GraphArgumentsDefinition : GraphNode
    {
        /// <summary>
        /// List of arguemnts in the list.
        /// </summary>
        public List<GraphArgumentDefinition> Arguments { get; set; } = new List<GraphArgumentDefinition>();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphArgumentsDefinition]";
        }
    }
}