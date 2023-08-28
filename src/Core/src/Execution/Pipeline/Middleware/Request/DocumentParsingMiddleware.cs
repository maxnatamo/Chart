using Chart.Core.Instrumentation;

namespace Chart.Core.Execution
{
    /// <summary>
    /// Middleware for parsing the document into graph definitions.
    /// </summary>
    public class DocumentParsingMiddleware : IRequestExecutionMiddleware
    {
        private readonly IExecutionEventRaiser _executionEventRaiser;

        public DocumentParsingMiddleware(IExecutionEventRaiser executionEventRaiser)
            => this._executionEventRaiser = executionEventRaiser;

        /// <inheritdoc />
        public async ValueTask<ExecutionResult> InvokeAsync(
            QueryExecutionContext context,
            RequestExecutionDelegate next,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this._executionEventRaiser.ParseDocument(context);

            if(!context.Query.Definitions.Any() && context.Query is DefaultQueryRequest query)
            {
                query.Parse();
            }

            this._executionEventRaiser.ParseDocumentFinished(context);

            return await next(context, cancellationToken);
        }
    }
}