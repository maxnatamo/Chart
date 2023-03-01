namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of an argument in an argument list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Argument">Original documentation</seealso>
    public class GraphArguments : GraphNode
    {
        /// <summary>
        /// Set the kind of argument.
        /// </summary>
        public List<GraphArgument> Arguments { get; set; } = new List<GraphArgument>();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphArguments]";
        }
    }
}