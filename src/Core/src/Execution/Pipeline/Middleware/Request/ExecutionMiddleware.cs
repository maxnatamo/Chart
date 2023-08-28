using Chart.Language.SyntaxTree;

namespace Chart.Core.Execution
{
    /// <summary>
    /// Middleware for executing the query.
    /// </summary>
    public class ExecutionMiddleware : IRequestExecutionMiddleware
    {
        private readonly IFieldExecutor _fieldExecutor;

        public ExecutionMiddleware(IFieldExecutor fieldExecutor)
            => this._fieldExecutor = fieldExecutor;

        /// <inheritdoc />
        public async ValueTask<ExecutionResult> InvokeAsync(
            QueryExecutionContext context,
            RequestExecutionDelegate next,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<GraphFieldSelection> selections = context.Operation.Selections.Selections
                .Cast<GraphFieldSelection>();

            foreach(GraphFieldSelection selection in selections)
            {
                string responseKey = selection.Alias ?? selection.Name;
                object? result = await this._fieldExecutor.ExecuteSelectionAsync(
                    context,
                    selection,
                    cancellationToken);

                context.Result.Data.Add(responseKey, result);
            }

            return context.Result;
        }
    }
}