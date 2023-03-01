namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a variable value for a variable.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Value">Original documentation</seealso>
    public abstract class GraphValue : GraphNode
    {
        /// <summary>
        /// Set the kind of the node to Value.
        /// </summary>
        public abstract GraphValueKind ValueKind { get; }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public abstract override string ToString();
    }
}