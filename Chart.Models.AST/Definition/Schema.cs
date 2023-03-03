namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a schema in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#SchemaDefinition">Original documentation</seealso>
    public class GraphSchemaDefinition : GraphDefinition
    {
        /// <summary>
        /// Set the kind of definition.
        /// </summary>
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Schema;

        /// <summary>
        /// Whether the definition is executable, as per the GraphQL-spec.
        /// </summary>
        public override bool Executable => false;

        /// <summary>
        /// The optional directives for the definition.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// Values in the schema.
        /// </summary>
        public GraphSchemaValues Values { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphSchemaDefinition]";
        }
    }
}