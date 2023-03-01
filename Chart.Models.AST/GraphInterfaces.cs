namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of interface implementations in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#ImplementsInterfaces">Original documentation</seealso>
    public class GraphInterfaces : GraphNode
    {
        /// <summary>
        /// The underlying list of implemented types.
        /// </summary>
        public List<GraphNamedType> Implements { get; set; } = new List<GraphNamedType>();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphInterfaces]";
        }
    }
}