namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for an int.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#IntValue">Original documentation</seealso>
    public class GraphIntValue : IGraphValue<int>
    {
        /// <inheritdoc />
        public GraphValueKind ValueKind => GraphValueKind.Int;

        /// <inheritdoc />
        public int Value { get; set; } = 0;

        /// <inheritdoc />
        object? IGraphValue.Value => this.Value;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphIntValue without a value.
        /// </summary>
        public GraphIntValue()
        { }

        /// <summary>
        /// Create a new GraphIntValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphIntValue-node.</param>
        public GraphIntValue(int value)
        {
            this.Value = value;
        }

        /// <inheritdoc />
        public override string ToString() => "[GraphIntValue]";
    }
}