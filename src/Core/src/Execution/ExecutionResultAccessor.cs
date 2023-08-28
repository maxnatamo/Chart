namespace Chart.Core
{
    /// <summary>
    /// Accessor for getting the current result of the request.
    /// </summary>
    public class ExecutionResultAccessor
    {
        public ExecutionResult Result { get; internal set; } = default!;
    }
}