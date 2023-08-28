namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for a string.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#StringValue">Original documentation</seealso>
    public class GraphStringValue : IGraphValue<string>
    {
        /// <inheritdoc />
        public GraphValueKind ValueKind => GraphValueKind.String;

        /// <inheritdoc />
        public string Value { get; set; } = "";

        /// <inheritdoc />
        object? IGraphValue.Value => this.Value;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphStringValue without a value.
        /// </summary>
        public GraphStringValue()
        { }

        /// <summary>
        /// Create a new GraphStringValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphStringValue-node.</param>
        public GraphStringValue(string value)
        {
            this.Value = value;
        }

        /// <inheritdoc />
        public override string ToString() => "[GraphStringValue]";
    }
}