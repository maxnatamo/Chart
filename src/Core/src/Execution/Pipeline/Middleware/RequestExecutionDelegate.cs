namespace Chart.Core
{
    public delegate ValueTask<ExecutionResult> RequestExecutionDelegate(
        QueryExecutionContext context,
        CancellationToken cancellationToken);
}