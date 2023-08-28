namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a variable value for an object.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ObjectValue">Original documentation</seealso>
    public class GraphObjectValue : IGraphValue<Dictionary<GraphName, IGraphValue>>
    {
        /// <inheritdoc />
        public GraphValueKind ValueKind => GraphValueKind.Object;

        /// <inheritdoc />
        public Dictionary<GraphName, IGraphValue> Value { get; }

        /// <inheritdoc />
        object? IGraphValue.Value => this.Value;

        /// <inheritdoc />
        public GraphLocation? Location { get; set; } = null;

        /// <summary>
        /// Create a new GraphObjectValue without a value.
        /// </summary>
        public GraphObjectValue()
            => this.Value = new Dictionary<GraphName, IGraphValue>();

        /// <summary>
        /// Create a new GraphObjectValue with the specified value.
        /// </summary>
        /// <param name="fields">The fields of the GraphObjectValue-node.</param>
        public GraphObjectValue(Dictionary<GraphName, IGraphValue> fields)
            => this.Value = fields;

        /// <inheritdoc />
        public override string ToString() => "[GraphObjectValue]";
    }
}