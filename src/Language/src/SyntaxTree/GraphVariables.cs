namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable in a variable list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#VariableDefinition">Original documentation</seealso>
    public class GraphVariables : IGraphNode
    {
        /// <summary>
        /// The optional variables in the definition.
        /// </summary>
        public List<GraphVariable> Variables { get; set; } = new List<GraphVariable>();

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => $"[GraphVariablesDefinition]";
    }
}