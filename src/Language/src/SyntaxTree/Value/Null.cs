namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for null.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#NullValue">Original documentation</seealso>
    public class GraphNullValue : IGraphValue<object?>
    {
        /// <inheritdoc />
        public GraphValueKind ValueKind => GraphValueKind.Null;

        /// <inheritdoc />
        public object? Value => null;

        /// <inheritdoc />
        object? IGraphValue.Value => this.Value;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphNullValue.
        /// </summary>
        public GraphNullValue()
        { }

        /// <inheritdoc />
        public override string ToString() => "[GraphNullValue]";
    }
}