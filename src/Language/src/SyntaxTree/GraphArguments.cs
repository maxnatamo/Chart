namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an argument in an argument list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Arguments">Original documentation</seealso>
    public class GraphArguments : IGraphNode
    {
        /// <summary>
        /// Set the kind of argument.
        /// </summary>
        public List<GraphArgument> Arguments { get; set; } = new List<GraphArgument>();

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphArguments]";
    }
}