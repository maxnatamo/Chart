using Chart.Language.SyntaxTree;

namespace Chart.Core.Execution
{
    /// <summary>
    /// Resolver for selection an operation in the query.
    /// </summary>
    public interface IOperationResolver
    {
        /// <summary>
        /// Resolve the corresponding operation from the query, given the execution context.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The operation should not be set in the context, when resolved. It should instead be returned.
        /// </para>
        /// <para>
        /// If an error occurs when finding the operation, an error will be raised.
        /// </para>
        /// </remarks>
        /// <param name="context">The operation context to derive the operation from.</param>
        /// <returns>The resolved operation definition.</returns>
        GraphOperationDefinition? Resolve(QueryExecutionContext context);
    }

    /// <inheritdoc cref="IOperationResolver" />
    public class DefaultOperationResolver : IOperationResolver
    {
        /// <inheritdoc />
        public GraphOperationDefinition? Resolve(QueryExecutionContext context)
        {
            if(!context.Query.TryGetDefinition(
                context.Query.OperationName,
                out GraphOperationDefinition? foundDefinition))
            {
                Error error = new ErrorBuilder()
                    .SetCode(ErrorCodes.OperationNotFound)
                    .SetMessage($"No operation with the name '{context.Query.OperationName}' was found on the query.")
                    .SetExtension("operation", context.Query.OperationName)
                    .Build();

                context.RaiseRequestError(error);
                return null;
            }

            return foundDefinition;
        }
    }
}