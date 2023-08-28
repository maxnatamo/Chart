namespace Chart.Core
{
    public interface IExecutionPipeline
    {
        /// <summary>
        /// Execute the given execution context and return it's result.
        /// </summary>
        /// <param name="context">The execution to execute.</param>
        /// <param name="cancellationToken">Cancellation token for aborting the request.</param>
        ValueTask<ExecutionResult> ExecuteAsync(
            QueryExecutionContext context,
            CancellationToken cancellationToken);
    }
}