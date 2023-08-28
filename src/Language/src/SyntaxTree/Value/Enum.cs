namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for enum.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#EnumValue">Original documentation</seealso>
    public class GraphEnumValue : IGraphValue<GraphName>
    {
        /// <inheritdoc />
        public GraphValueKind ValueKind => GraphValueKind.Enum;

        /// <inheritdoc />
        public GraphName Value { get; set; } = default!;

        /// <inheritdoc />
        object? IGraphValue.Value => this.Value;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphEnumValue without a value.
        /// </summary>
        public GraphEnumValue()
        { }

        /// <summary>
        /// Create a new GraphEnumValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphEnumValue-node.</param>
        public GraphEnumValue(string value)
        {
            this.Value = new GraphName(value);
        }

        /// <summary>
        /// Create a new GraphEnumValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphEnumValue-node.</param>
        public GraphEnumValue(GraphName value)
        {
            this.Value = value;
        }

        /// <inheritdoc />
        public override string ToString() => "[GraphEnumValue]";
    }
}