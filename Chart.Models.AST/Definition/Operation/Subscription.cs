namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a subscription operation in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
    public class GraphSubscriptionOperation : GraphOperationDefinition
    {
        /// <summary>
        /// Set the type of the operation to Subscription.
        /// </summary>
        public override GraphOperationKind OperationKind => GraphOperationKind.Subscription;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphOperation] Subscription";
        }
    }
}