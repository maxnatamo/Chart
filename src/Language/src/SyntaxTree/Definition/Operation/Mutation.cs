namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a mutation operation in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
    public class GraphMutationOperation : GraphOperationDefinition
    {
        /// <inheritdoc />
        public override GraphOperationKind OperationKind => GraphOperationKind.Mutation;

        /// <inheritdoc />
        public override string ToString() => $"[GraphOperation] Mutation";
    }
}