namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an argument in an argument list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Argument">Original documentation</seealso>
    public class GraphArgument : IGraphNode, IHasName
    {
        /// <summary>
        /// Set the name of the argument.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// Set the value of the argument.
        /// </summary>
        public IGraphValue Value { get; set; } = default!;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <inheritdoc />
        public override string ToString() => "[GraphArgument]";
    }
}