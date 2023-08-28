using Chart.Language.SyntaxTree;

using Microsoft.Extensions.Options;

namespace Chart.Core.Execution
{
    /// <summary>
    /// Middleware for resolving the correct operation from the query, if multiple operations are given.
    /// </summary>
    public class ExceptionHandlingMiddleware : IRequestExecutionMiddleware
    {
        private readonly IOptions<GraphOptions> _options;

        public ExceptionHandlingMiddleware(IOptions<GraphOptions> options)
            => this._options = options;

        /// <inheritdoc />
        public async ValueTask<ExecutionResult> InvokeAsync(
            QueryExecutionContext context,
            RequestExecutionDelegate next,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await next(context, cancellationToken);
            }
            catch(Exception e)
            {
                Error error = new ErrorBuilder()
                    .SetMessage("Unhandled exception occured while executing the request.")
                    .SetException(e)
                    .Build();

                if(!this._options.Value.Request.IncludeExceptions)
                {
                    error.Exception = null;
                }

                context.RaiseFieldError(error);
                return context.Result;
            }
        }
    }
}