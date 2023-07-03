namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a subscription operation in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
    public class GraphSubscriptionOperation : GraphOperationDefinition
    {
        /// <inheritdoc />
        public override GraphOperationKind OperationKind => GraphOperationKind.Subscription;

        /// <inheritdoc />
        public override string ToString() => $"[GraphOperation] Subscription";
    }
}