namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of an argument in an argument list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Argument">Original documentation</seealso>
    public class GraphArgumentDefinition : GraphNode
    {
        /// <summary>
        /// An optional description of the argument.
        /// </summary>
        public GraphDescription? Description { get; set; } = null;

        /// <summary>
        /// The name of the variable.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// Set the kind of argument.
        /// </summary>
        public GraphType Type { get; set; } = default!;

        /// <summary>
        /// The optional default value of the argument.
        /// </summary>
        public GraphValue? DefaultValue { get; set; } = null;

        /// <summary>
        /// The optional directives of the argument.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphArgumentDefinition]";
        }
    }
}