namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for a variable.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Variable">Original documentation</seealso>
    public class GraphVariableValue : IGraphValue<GraphName>
    {
        /// <inheritdoc />
        public GraphValueKind ValueKind => GraphValueKind.Variable;

        /// <inheritdoc />
        public GraphName Value { get; set; } = default!;

        /// <inheritdoc />
        object? IGraphValue.Value => this.Value;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphVariableValue without a value.
        /// </summary>
        public GraphVariableValue()
        { }

        /// <summary>
        /// Create a new GraphVariableValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphVariableValue-node.</param>
        public GraphVariableValue(string value)
        {
            this.Value = new GraphName(value);
        }

        /// <summary>
        /// Create a new GraphVariableValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphVariableValue-node.</param>
        public GraphVariableValue(GraphName value)
        {
            this.Value = value;
        }

        /// <inheritdoc />
        public override string ToString() => "[GraphVariableValue]";
    }
}