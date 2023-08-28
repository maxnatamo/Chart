namespace Chart.Core.Instrumentation
{
    /// <summary>
    /// Middleware for resolving a field.
    /// </summary>
    public class FieldExecutionMiddleware : IFieldExecutionMiddleware
    {
        /// <inheritdoc />
        public async ValueTask<object?> InvokeAsync(
            ResolverContext context,
            FieldExecutionDelegate next,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            object? resolvedValue = context.Field.Resolver!(context);
            return await ValueTask.FromResult(resolvedValue);
        }
    }
}