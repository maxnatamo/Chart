namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a mutation operation in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
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