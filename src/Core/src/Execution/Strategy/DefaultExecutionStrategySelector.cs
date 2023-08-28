namespace Chart.Core.Execution
{
    /// <inheritdoc cref="IExecutionStrategySelector" />
    public class DefaultExecutionStrategySelector : IExecutionStrategySelector
    {
        /// <inheritdoc />
        public IExecutionStrategyImplementation Select(ExecutionStrategy strategy) =>
            strategy switch
            {
                ExecutionStrategy.Serial => new SerialExecutionStrategyImplementation(),
                ExecutionStrategy.Parallel => new ParallelExecutionStrategyImplementation(),

                _ => throw new NotSupportedException($"Execution strategy not implemented: {strategy}")
            };
    }
}