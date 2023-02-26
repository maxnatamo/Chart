namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a variable value for a boolean.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#BooleanValue">Original documentation</seealso>
    public class GraphBooleanValue : GraphValue
    {
        /// <summary>
        /// Set the kind of the value to Boolean.
        /// </summary>
        public override GraphValueKind ValueKind => GraphValueKind.Boolean;

        /// <summary>
        /// The underlying value.
        /// </summary>
        public bool Value { get; set; } = false;

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
        {
            this.Value = value;
        }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphBooleanValue]";
        }
    }
}