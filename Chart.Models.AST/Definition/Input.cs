namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of an input-object in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#InputObjectTypeDefinition">Original documentation</seealso>
    public class GraphInputDefinition : GraphDefinition
    {
        /// <summary>
        /// Set the kind of operation.
        /// </summary>
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Input;

        /// <summary>
        /// Optional directives for the type.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <summary>
        /// Underlying arguments of the type definition.
        /// </summary>
        public GraphArgumentsDefinition Arguments { get; set; } = default!;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphInputDefinition]";
        }
    }
}