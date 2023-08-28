namespace Chart.Core
{
    public delegate ValueTask<object?> FieldExecutionDelegate(
        ResolverContext context,
        CancellationToken cancellationToken);
}