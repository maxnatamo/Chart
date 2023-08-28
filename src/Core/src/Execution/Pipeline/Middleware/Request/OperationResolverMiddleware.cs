using Chart.Language.SyntaxTree;

namespace Chart.Core.Execution
{
    /// <summary>
    /// Middleware for resolving the correct operation from the query, if multiple operations are given.
    /// </summary>
    public class OperationResolverMiddleware : IRequestExecutionMiddleware
    {
        private readonly IOperationResolver _operationResolver;
        private readonly IServiceProvider _serviceProvider;

        public OperationResolverMiddleware(
            IOperationResolver operationResolver,
            IServiceProvider serviceProvider)
        {
            this._operationResolver = operationResolver;
            this._serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public async ValueTask<ExecutionResult> InvokeAsync(
            QueryExecutionContext context,
            RequestExecutionDelegate next,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            GraphOperationDefinition? operation = this._operationResolver.Resolve(context);
            if(operation is null)
            {
                throw new Exception();
            }

            context.Operation = operation;
            this.ResolveRootValue(context);

            return await next(context, cancellationToken);
        }

        private void ResolveRootValue(QueryExecutionContext context)
        {
            ObjectType? rootType = context.Operation switch
            {
                GraphQueryOperation => context.Schema.QueryType,
                GraphMutationOperation => context.Schema.MutationType,
                GraphSubscriptionOperation => context.Schema.SubscriptionType,

                _ => throw new NotSupportedException()
            };

            if(rootType is null || rootType.RuntimeType is null)
            {
                throw new NotImplementedException(context.Operation.OperationKind.ToString());
            }

            context.RootType = rootType;
            context.RootGraphType = context.RootType.CreateSyntaxNode(this._serviceProvider);

            context.RootValue = this._serviceProvider.GetService(rootType.RuntimeType)
                ?? throw new NotImplementedException(rootType.RuntimeType.ToString());
        }
    }
}