namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a query operation in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
    public class GraphQueryOperation : GraphOperationDefinition
    {
        /// <inheritdoc />
        public override GraphOperationKind OperationKind => GraphOperationKind.Query;

        /// <inheritdoc />
        public override string ToString() => $"[GraphOperation] Query";
    }
}