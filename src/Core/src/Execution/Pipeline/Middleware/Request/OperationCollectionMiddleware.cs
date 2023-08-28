using Chart.Language.SyntaxTree;

namespace Chart.Core.Execution
{
    /// <summary>
    /// Middleware for merging/collecting all the selections inthe operation into pure field selections.
    /// </summary>
    public class OperationCollectionMiddleware : IRequestExecutionMiddleware
    {
        private readonly IFieldCollector _fieldCollector;

        public OperationCollectionMiddleware(IFieldCollector fieldCollector)
            => this._fieldCollector = fieldCollector;

        /// <inheritdoc />
        public async ValueTask<ExecutionResult> InvokeAsync(
            QueryExecutionContext context,
            RequestExecutionDelegate next,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            context.Operation.Selections.Selections = this._fieldCollector
                .CollectFields(context)
                .Cast<GraphSelection>()
                .ToList();

            return await next(context, cancellationToken);
        }
    }
}