namespace Chart.Core
{
    public class ExecutionResult
    {
        /// <summary>
        /// Query response data.
        /// </summary>
        public Dictionary<string, object?> Data { get; } = new();

        /// <summary>
        /// List of any errors that occurred during execution.
        /// </summary>
        public List<Error> Errors { get; } = new();

        /// <summary>
        /// Optional extensions to the response.
        /// </summary>
        public Dictionary<string, object?> Extensions { get; } = new();
    }
}