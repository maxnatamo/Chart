namespace Chart.Models.AST
{
    /// <summary>
    /// Directive definition in the schema.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#DirectiveDefinition">Original documentation</seealso>
    public class GraphDirectiveDefinition : GraphDefinition
    {
        /// <summary>
        /// Set the kind of operation.
        /// </summary>
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Directive;

        /// <summary>
        /// Whether the definition is executable, as per the GraphQL-spec.
        /// </summary>
        public override bool Executable => false;

        /// <summary>
        /// Optional arguments for the directive.
        /// </summary>
        public GraphArgumentsDefinition? Arguments { get; set; } = null;

        /// <summary>
        /// Whether the directive is repeatable.
        /// </summary>
        public bool Repeatable { get; set; } = false;

        /// <summary>
        /// List of directive locations selected by the directive.
        /// </summary>
        public GraphDirectiveLocations Locations { get; set; } = new GraphDirectiveLocations();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphDirectiveDefinition]";
        }
    }
}