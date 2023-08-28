namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a schema in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#SchemaDefinition">Original documentation</seealso>
    public class GraphSchemaDefinition : GraphDefinition, IHasDirectives
    {
        /// <inheritdoc />
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Schema;

        /// <inheritdoc />
        public override bool Executable => false;

        /// <summary>
        /// The optional directives for the definition.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// Values in the schema.
        /// </summary>
        public GraphSchemaValues Values { get; set; } = default!;

        /// <inheritdoc />
        public override string ToString() => "[GraphSchemaDefinition]";
    }
}