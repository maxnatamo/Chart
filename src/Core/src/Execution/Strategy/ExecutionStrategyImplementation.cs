namespace Chart.Core.Execution
{
    /// <summary>
    /// Contract for execution strategy implementations.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Tasks to added to the strategy implementation will be executed based on the strategy (serial/parallel).
    /// </para>
    /// <para>
    /// To avoid tasks starting preemtively, <see cref="AddTask(Func{Task})" /> expects a function that returns a <see cref="Task"/>.
    /// Tasks returned from these functions will be awaited by the strategy implementation.
    /// Therefore, they should never be <see langword="async" />, as it would cause an infinite loop.
    /// </para>
    /// </remarks>
    public interface IExecutionStrategyImplementation
    {
        /// <summary>
        /// Add a task to be executed by the strategy, without a return type.
        /// </summary>
        void AddTask(Func<Task> task);

        /// <summary>
        /// Add a task to be executed by the strategy, with a return type of <typeparamref name="T" />.
        /// </summary>
        void AddTask<T>(Func<Task<T>> task);

        /// <summary>
        /// Add a value-task to be executed by the strategy, without a return type.
        /// </summary>
        void AddTask(Func<ValueTask> task);

        /// <summary>
        /// Add a value-task to be executed by the strategy, with a return type of <typeparamref name="T" />.
        /// </summary>
        void AddTask<T>(Func<ValueTask<T>> task);

        /// <summary>
        /// Execute the task pool, without getting the return values.
        /// </summary>
        /// <param name="cancellationToken">Token for cancelling the execution.</param>
        Task Execute(CancellationToken? cancellationToken = null);

        /// <summary>
        /// Execute the task pool, and returns all the return values from the tasks.
        /// </summary>
        /// <remarks>
        /// Only tasks and value-tasks with the same type parameter are executed.
        /// </remarks>
        /// <param name="cancellationToken">Token for cancelling the execution.</param>
        /// <returns>List of all return values, in the same order as the tasks.</returns>
        Task<IEnumerable<T>> ExecuteResult<T>(CancellationToken? cancellationToken = null);
    }
}