namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of an operation in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Language.Operations">Original documentation</seealso>
    public abstract class GraphOperationDefinition : GraphDefinition, IHasDirectives
    {
        /// <inheritdoc />
        public override GraphDefinitionKind DefinitionKind => GraphDefinitionKind.Operation;

        /// <inheritdoc />
        public override bool Executable => true;

        /// <summary>
        /// The type of operation.
        /// </summary>
        public abstract GraphOperationKind OperationKind { get; }

        /// <summary>
        /// Set of selections for the operation.
        /// </summary>
        public GraphSelectionSet Selections { get; set; } = new GraphSelectionSet();

        /// <summary>
        /// The optional variables in the definition.
        /// </summary>
        public GraphVariables? Variables { get; set; } = null;

        /// <summary>
        /// Optional directives for the operation.
        /// </summary>
        public GraphDirectives? Directives { get; set; } = null;

        /// <inheritdoc />
        public abstract override string ToString();
    }
}