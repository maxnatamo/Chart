using System.Collections.Concurrent;

namespace Chart.Core.Execution
{
    /// <summary>
    /// Base implementation for execution strategies.
    /// </summary>
    public abstract class ExecutionStrategyImplementationBase : IExecutionStrategyImplementation
    {
        /// <summary>
        /// Queue of tasks to execute.
        /// </summary>
        protected readonly ConcurrentQueue<Func<Task>> Tasks = new();

        /// <inheritdoc />
        public virtual void AddTask(Func<Task> task)
            => this.Tasks.Enqueue(task);

        /// <inheritdoc />
        public virtual void AddTask<T>(Func<Task<T>> task)
            => this.Tasks.Enqueue(task);

        /// <inheritdoc />
        public virtual void AddTask(Func<ValueTask> task)
            => this.Tasks.Enqueue(() => task().AsTask());

        /// <inheritdoc />
        public virtual void AddTask<T>(Func<ValueTask<T>> task)
            => this.Tasks.Enqueue(() => task().AsTask());

        /// <inheritdoc />
        public abstract Task Execute(CancellationToken? cancellationToken = null);

        /// <inheritdoc />
        public abstract Task<IEnumerable<T>> ExecuteResult<T>(CancellationToken? cancellationToken = null);
    }
}