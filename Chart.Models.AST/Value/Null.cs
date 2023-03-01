namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a variable value for null.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#NullValue">Original documentation</seealso>
    public class GraphNullValue : GraphValue
    {
        /// <summary>
        /// Set the kind of the value to Null.
        /// </summary>
        public override GraphValueKind ValueKind => GraphValueKind.Null;

        /// <summary>
        /// Create a new GraphNullValue.
        /// </summary>
        public GraphNullValue()
        { }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphNullValue]";
        }
    }
}