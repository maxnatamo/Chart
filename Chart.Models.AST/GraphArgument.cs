namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of an argument in an argument list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Argument">Original documentation</seealso>
    public class GraphArgument : GraphNode
    {
        /// <summary>
        /// Set the name of the argument.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// Set the value of the argument.
        /// </summary>
        public GraphValue Value { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphArgument]";
        }
    }
}