namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a variable value for a list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ListValue">Original documentation</seealso>
    public class GraphListValue : GraphValue
    {
        /// <summary>
        /// Set the kind of the value to List.
        /// </summary>
        public override GraphValueKind ValueKind => GraphValueKind.List;

        /// <summary>
        /// The underlying values in the list.
        /// </summary>
        public List<GraphValue> Values { get; set; } = new List<GraphValue>();

        /// <summary>
        /// Create a new GraphListValue without a value.
        /// </summary>
        public GraphListValue()
        { }

        /// <summary>
        /// Create a new GraphListValue with the specified value.
        /// </summary>
        /// <param name="values">The values of the GraphListValue-node.</param>
        public GraphListValue(List<GraphValue> values)
        {
            this.Values = values;
        }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphListValue]";
        }
    }
}