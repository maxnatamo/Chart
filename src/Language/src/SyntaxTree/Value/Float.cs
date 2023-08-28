namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for a float.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#FloatValue">Original documentation</seealso>
    public class GraphFloatValue : IGraphValue<double>
    {
        /// <inheritdoc />
        public GraphValueKind ValueKind => GraphValueKind.Float;

        /// <inheritdoc />
        public double Value { get; set; } = 0;

        /// <inheritdoc />
        object? IGraphValue.Value => this.Value;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphFloatValue without a value.
        /// </summary>
        public GraphFloatValue()
        { }

        /// <summary>
        /// Create a new GraphFloatValue with the specified value.
        /// </summary>
        public GraphFloatValue(double value)
            => this.Value = value;

        /// <inheritdoc />
        public override string ToString() => "[GraphFloatValue]";
    }
}