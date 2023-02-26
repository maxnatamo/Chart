namespace Chart.Core.Parser
{
    /// <summary>
    /// Definition of an operation in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#sec-Language.Operations">Original documentation</seealso>
    public class GraphMutationOperation : GraphOperationDefinition
    {
        /// <summary>
        /// Set the type of the operation to Mutation.
        /// </summary>
        public override GraphOperationKind OperationKind => GraphOperationKind.Mutation;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphOperation] Mutation";
        }
    }
}