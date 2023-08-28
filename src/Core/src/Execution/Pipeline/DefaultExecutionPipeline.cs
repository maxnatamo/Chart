using Chart.Core.Execution;

namespace Chart.Core
{
    public class DefaultExecutionPipeline : IExecutionPipeline
    {
        private int MiddlewareIndex = 0;

        private readonly List<IRequestExecutionMiddleware> _requestMiddlewares;

        public DefaultExecutionPipeline(
            IEnumerable<IRequestExecutionMiddleware> requestMiddlewares)
        {
            this.MiddlewareIndex = 0;
            this._requestMiddlewares = requestMiddlewares.ToList();
        }

        /// <summary>
        /// Execute the given execution context and return it's result.
        /// </summary>
        /// <param name="context">The execution to execute.</param>
        public virtual async ValueTask<ExecutionResult> ExecuteAsync(
            QueryExecutionContext context,
            CancellationToken cancellationToken)
        {
            ExecutionResult result = await this.InvokeAsync(context, cancellationToken);

            result.Errors.AddRange(context.FieldErrors);
            result.Errors.AddRange(context.RequestErrors);

            return result;
        }

        private async ValueTask<ExecutionResult> InvokeAsync(
            QueryExecutionContext context,
            CancellationToken cancellationToken)
        {
            if(this._requestMiddlewares.Count <= this.MiddlewareIndex)
            {
                string? middlewareName = this._requestMiddlewares.LastOrDefault()?.GetType().Name;

                throw new MiddlewareException(
                    middlewareName ?? "(null)",
                    "Last middleware failed to return a value.");
            }

            IRequestExecutionMiddleware middleware = this._requestMiddlewares[this.MiddlewareIndex++];

            return await middleware.InvokeAsync(
                context,
                this.InvokeAsync,
                cancellationToken);
        }
    }
}