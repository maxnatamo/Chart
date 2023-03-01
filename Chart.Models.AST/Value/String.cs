namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a variable value for a string.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#StringValue">Original documentation</seealso>
    public class GraphStringValue : GraphValue
    {
        /// <summary>
        /// Set the kind of the value to String.
        /// </summary>
        public override GraphValueKind ValueKind => GraphValueKind.String;

        /// <summary>
        /// The underlying value.
        /// </summary>
        public string Value { get; set; } = "";

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

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphStringValue]";
        }
    }
}