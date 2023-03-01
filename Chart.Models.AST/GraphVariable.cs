namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a variable in a variable list.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#VariableDefinition">Original documentation</seealso>
    public class GraphVariable : GraphNode
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// The type of the variable.
        /// </summary>
        public GraphType Type { get; set; } = default!;

        /// <summary>
        /// The default value of the variable.
        /// </summary>
        public GraphValue? DefaultValue { get; set; } = null;

        /// <summary>
        /// Optional directives for the variable.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphVariableDefinition]";
        }
    }
}