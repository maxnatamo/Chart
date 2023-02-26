namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a variable value for an object.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ObjectValue">Original documentation</seealso>
    public class GraphObjectValue : GraphValue
    {
        /// <summary>
        /// Set the kind of the value to Object.
        /// </summary>
        public override GraphValueKind ValueKind => GraphValueKind.Object;

        /// <summary>
        /// The underlying fields in the object.
        /// </summary>
        public Dictionary<GraphName, GraphValue> Fields { get; set; } = new Dictionary<GraphName, GraphValue>();

        /// <summary>
        /// Create a new GraphObjectValue without a value.
        /// </summary>
        public GraphObjectValue()
        { }

        /// <summary>
        /// Create a new GraphObjectValue with the specified value.
        /// </summary>
        /// <param name="fields">The fields of the GraphObjectValue-node.</param>
        public GraphObjectValue(Dictionary<GraphName, GraphValue> fields)
        {
            this.Fields = fields;
        }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphObjectValue]";
        }
    }
}