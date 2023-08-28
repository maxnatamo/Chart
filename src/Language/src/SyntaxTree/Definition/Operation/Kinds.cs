namespace Chart.Language.SyntaxTree
{
    public enum GraphOperationKind
    {
        /// <summary>
        /// Definition of a query operation in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
        Query,

        /// <summary>
        /// Definition of a mutation operation in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
        Mutation,

        /// <summary>
        /// Definition of a subscription operation in the root GraphQL-document.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#OperationDefinition">Original documentation</seealso>
        Subscription
    }
}