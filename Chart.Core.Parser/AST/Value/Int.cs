namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a variable value for an int.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#IntValue">Original documentation</seealso>
    public class GraphIntValue : GraphValue
    {
        /// <summary>
        /// Set the kind of the value to Int.
        /// </summary>
        public override GraphValueKind ValueKind => GraphValueKind.Int;

        /// <summary>
        /// The underlying value.
        /// </summary>
        public int Value { get; set; } = 0;

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

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphIntValue]";
        }
    }
}