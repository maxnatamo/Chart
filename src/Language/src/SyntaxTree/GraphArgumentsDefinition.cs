namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a list of arguments in a type definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ArgumentsDefinition">Original documentation</seealso>
    public class GraphArgumentsDefinition : IGraphNode
    {
        /// <summary>
        /// List of arguemnts in the list.
        /// </summary>
        public List<GraphArgumentDefinition> Arguments { get; set; } = new List<GraphArgumentDefinition>();

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphArgumentsDefinition]";
    }
}