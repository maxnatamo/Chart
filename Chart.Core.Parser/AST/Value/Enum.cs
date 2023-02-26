namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a variable value for enum.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#EnumValue">Original documentation</seealso>
    public class GraphEnumValue : GraphValue
    {
        /// <summary>
        /// Set the kind of the value to Null.
        /// </summary>
        public override GraphValueKind ValueKind => GraphValueKind.Enum;

        /// <summary>
        /// The underlying value.
        /// </summary>
        public GraphName Value { get; set; } = default!;

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

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphEnumValue]";
        }
    }
}