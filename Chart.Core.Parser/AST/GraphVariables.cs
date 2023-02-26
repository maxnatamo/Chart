namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a variable in a variable list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#VariableDefinition">Original documentation</seealso>
    public class GraphVariables : GraphNode
    {
        /// <summary>
        /// The optional variables in the definition.
        /// </summary>
        public List<GraphVariable> Variables { get; set; } = new List<GraphVariable>();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphVariablesDefinition]";
        }
    }
}