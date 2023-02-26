namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of a field-list in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#FieldsDefinition">Original documentation</seealso>
    public class GraphFields : GraphNode
    {
        /// <summary>
        /// The underlying list of field definitions.
        /// </summary>
        public List<GraphField> Fields { get; set; } = new List<GraphField>();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphFields]";
        }
    }
}