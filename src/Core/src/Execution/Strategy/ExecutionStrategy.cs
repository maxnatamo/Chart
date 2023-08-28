namespace Chart.Core.Execution
{
    /// <summary>
    /// Enumeration of available execution strategies for operations.
    /// </summary>
    public enum ExecutionStrategy
    {
        /// <summary>
        /// Execute tasks sequentially.
        /// </summary>
        Serial,

        /// <summary>
        /// Execute tasks in parallel.
        /// </summary>
        Parallel,
    }
}