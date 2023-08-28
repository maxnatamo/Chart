namespace Chart.Core.Execution
{
    /// <summary>
    /// Execution strategy implementation for parallel execution.
    /// </summary>
    public class ParallelExecutionStrategyImplementation : ExecutionStrategyImplementationBase
    {
        /// <inheritdoc />
        public override async Task Execute(CancellationToken? cancellationToken = null)
        {
            cancellationToken?.ThrowIfCancellationRequested();

            await Task.WhenAll(this.Tasks.Select(v => v()));
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<T>> ExecuteResult<T>(CancellationToken? cancellationToken = null)
        {
            List<Task<T>> tasksToExecute = new(capacity: this.Tasks.Count);

            while(this.Tasks.TryDequeue(out Func<Task>? task))
            {
                cancellationToken?.ThrowIfCancellationRequested();

                if(task is Func<Task<T>> resultTask)
                {
                    tasksToExecute.Add(resultTask());
                }
            }

            return await Task.WhenAll<T>(tasksToExecute);
        }
    }
}