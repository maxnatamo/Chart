namespace Chart.Core.Instrumentation
{
    public interface IExecutionEventRaiser
    {
        /// <inheritdoc cref="ExecutionEventListener.ParseDocument" />
        void ParseDocument(QueryExecutionContext context);

        /// <inheritdoc cref="ExecutionEventListener.ParseDocumentFinished" />
        void ParseDocumentFinished(QueryExecutionContext context);

        /// <inheritdoc cref="ExecutionEventListener.ValidateDocument" />
        void ValidateDocument(QueryExecutionContext context);

        /// <inheritdoc cref="ExecutionEventListener.ValidateDocumentFinished" />
        void ValidateDocumentFinished(QueryExecutionContext context);

        /// <inheritdoc cref="ExecutionEventListener.DocumentExecution" />
        void DocumentExecution(ResolverContext context);

        /// <inheritdoc cref="ExecutionEventListener.DocumentExecutionFinished" />
        void DocumentExecutionFinished(ResolverContext context);
    }

    public class ExecutionEventRaiser : IExecutionEventRaiser
    {
        private readonly IEnumerable<ExecutionEventListener> _executionEventListeners;

        public ExecutionEventRaiser(IEnumerable<ExecutionEventListener> executionEventListeners)
            => this._executionEventListeners = executionEventListeners;

        /// <inheritdoc />
        public void ParseDocument(QueryExecutionContext context)
            => this.RaiseEvent(v => v.ParseDocument(context));

        /// <inheritdoc />
        public void ParseDocumentFinished(QueryExecutionContext context)
            => this.RaiseEvent(v => v.ParseDocumentFinished(context));

        /// <inheritdoc />
        public void ValidateDocument(QueryExecutionContext context)
            => this.RaiseEvent(v => v.ValidateDocument(context));

        /// <inheritdoc />
        public void ValidateDocumentFinished(QueryExecutionContext context)
            => this.RaiseEvent(v => v.ValidateDocumentFinished(context));

        /// <inheritdoc />
        public void DocumentExecution(ResolverContext context)
            => this.RaiseEvent(v => v.DocumentExecution(context));

        /// <inheritdoc />
        public void DocumentExecutionFinished(ResolverContext context)
            => this.RaiseEvent(v => v.DocumentExecutionFinished(context));

        private void RaiseEvent(Action<ExecutionEventListener> action)
        {
            foreach(ExecutionEventListener listener in this._executionEventListeners)
            {
                action(listener);
            }
        }
    }
}