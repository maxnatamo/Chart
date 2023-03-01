namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a variable value for a variable.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Variable">Original documentation</seealso>
    public class GraphVariableValue : GraphValue
    {
        /// <summary>
        /// Set the kind of the value to Variable.
        /// </summary>
        public override GraphValueKind ValueKind => GraphValueKind.Variable;

        /// <summary>
        /// The value of the variable.
        /// </summary>
        public GraphName Variable { get; set; } = default!;

        /// <summary>
        /// Create a new GraphVariableValue without a value.
        /// </summary>
        public GraphVariableValue()
        { }

        /// <summary>
        /// Create a new GraphVariableValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphVariableValue-node.</param>
        public GraphVariableValue(string value)
        {
            this.Variable = new GraphName(value);
        }

        /// <summary>
        /// Create a new GraphVariableValue with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphVariableValue-node.</param>
        public GraphVariableValue(GraphName value)
        {
            this.Variable = value;
        }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphVariableValue]";
        }
    }
}