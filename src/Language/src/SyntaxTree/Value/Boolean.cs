namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for a boolean.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#BooleanValue">Original documentation</seealso>
    public class GraphBooleanValue : IGraphValue<bool>
    {
        /// <inheritdoc />
        public GraphValueKind ValueKind => GraphValueKind.Boolean;

        /// <inheritdoc />
        public bool Value { get; set; } = false;

        /// <inheritdoc />
        object? IGraphValue.Value => this.Value;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphBooleanValue without a value.
        /// </summary>
        public GraphBooleanValue()
        { }

        /// <summary>
        /// Create a new GraphBooleanValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphBooleanValue-node.</param>
        public GraphBooleanValue(bool value)
            => this.Value = value;

        /// <inheritdoc />
        public override string ToString() => "[GraphBooleanValue]";
    }
}