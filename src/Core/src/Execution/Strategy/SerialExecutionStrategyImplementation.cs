namespace Chart.Core.Execution
{
    /// <summary>
    /// Execution strategy implementation for sequential execution.
    /// </summary>
    public class SerialExecutionStrategyImplementation : ExecutionStrategyImplementationBase
    {
        /// <inheritdoc />
        public override async Task Execute(CancellationToken? cancellationToken = null)
        {
            while(this.Tasks.TryDequeue(out Func<Task>? task))
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await task();
            }
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<T>> ExecuteResult<T>(CancellationToken? cancellationToken = null)
        {
            List<T> results = new(capacity: this.Tasks.Count);

            while(this.Tasks.TryDequeue(out Func<Task>? task))
            {
                cancellationToken?.ThrowIfCancellationRequested();

                if(task is Func<Task<T>> resultTask)
                {
                    T result = await resultTask();
                    results.Add(result);
                }
            }

            return results;
        }
    }
}