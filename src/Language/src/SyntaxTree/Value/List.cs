namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for a list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ListValue">Original documentation</seealso>
    public class GraphListValue : IGraphValue<List<IGraphValue>>
    {
        /// <inheritdoc />
        public GraphValueKind ValueKind => GraphValueKind.List;

        /// <inheritdoc />
        public List<IGraphValue> Value { get; }

        /// <inheritdoc />
        object? IGraphValue.Value => this.Value;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphListValue without a value.
        /// </summary>
        public GraphListValue()
            => this.Value = new List<IGraphValue>();

        /// <summary>
        /// Create a new GraphListValue with the specified value.
        /// </summary>
        /// <param name="values">The values of the GraphListValue-node.</param>
        public GraphListValue(List<IGraphValue> values)
            => this.Value = values;

        /// <inheritdoc />
        public override string ToString() => "[GraphListValue]";
    }
}