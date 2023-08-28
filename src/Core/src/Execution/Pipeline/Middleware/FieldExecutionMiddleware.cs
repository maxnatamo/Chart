namespace Chart.Core
{
    public interface IFieldExecutionMiddleware
    {
        ValueTask<object?> InvokeAsync(
            ResolverContext context,
            FieldExecutionDelegate next,
            CancellationToken cancelToken);
    }
}