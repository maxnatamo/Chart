namespace Chart.Core
{
    public interface IRequestExecutionMiddleware
    {
        ValueTask<ExecutionResult> InvokeAsync(
            QueryExecutionContext context,
            RequestExecutionDelegate next,
            CancellationToken cancelToken);
    }
}