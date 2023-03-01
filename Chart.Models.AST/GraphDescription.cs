namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a description in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Name">Original documentation</seealso>
    public class GraphDescription : GraphNode
    {
        /// <summary>
        /// The value of the name.
        /// </summary>
        public string Value { get; set; } = default!;

        /// <summary>
        /// Create a new GraphDescription with the specified value.
        /// </summary>
        /// <param name="value">The value of the GraphDescription-node.</param>
        public GraphDescription(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphDescription] {Value}";
        }
    }
}