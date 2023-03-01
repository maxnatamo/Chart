namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a variable value for a float.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#FloatValue">Original documentation</seealso>
    public class GraphFloatValue : GraphValue
    {
        /// <summary>
        /// Set the kind of the value to Float.
        /// </summary>
        public override GraphValueKind ValueKind => GraphValueKind.Float;

        /// <summary>
        /// The underlying value.
        /// </summary>
        public Double Value { get; set; } = 0;

        /// <summary>
        /// Create a new GraphFloatValue without a value.
        /// </summary>
        public GraphFloatValue()
        { }

        /// <summary>
        /// Create a new GraphFloatValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphFloatValue-node.</param>
        public GraphFloatValue(float value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphFloatValue]";
        }
    }
}