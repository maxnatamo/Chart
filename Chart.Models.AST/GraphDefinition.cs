namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a definition in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Definition">Original documentation</seealso>
    public abstract class GraphDefinition : GraphNode
    {
        /// <summary>
        /// Set the kind of definition.
        /// </summary>
        public abstract GraphDefinitionKind DefinitionKind { get; }

        /// <summary>
        /// Whether the definition is executable, as per the GraphQL-spec.
        /// </summary>
        public abstract bool Executable { get; }

        /// <summary>
        /// The optional name for the definition.
        /// </summary>
        public GraphName? Name { get; set; } = null;

        /// <summary>
        /// The optional description for the definition.
        /// </summary>
        public GraphDescription? Description { get; set; } = null;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public abstract override string ToString();
    }
}