namespace Chart.Core.Execution
{
    /// <summary>
    /// Selector for choosing the appropriate execution strategy implementation.
    /// </summary>
    public interface IExecutionStrategySelector
    {
        /// <summary>
        /// Select the appropriate execution strategy implementation for the given strategy.
        /// </summary>
        IExecutionStrategyImplementation Select(ExecutionStrategy strategy);
    }
}