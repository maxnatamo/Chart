namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a query operation in the root GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
    public class GraphQueryOperation : GraphOperationDefinition
    {
        /// <summary>
        /// Set the type of the operation to Query.
        /// </summary>
        public override GraphOperationKind OperationKind => GraphOperationKind.Query;

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphOperation] Query";
        }
    }
}