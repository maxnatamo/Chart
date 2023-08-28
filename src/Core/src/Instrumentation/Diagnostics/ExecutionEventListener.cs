namespace Chart.Core.Instrumentation
{
    /// <summary>
    /// Event listener logic to track execution diagnostics.
    /// </summary>
    public abstract class ExecutionEventListener : IEventListener
    {
        /// <summary>
        /// Raised before parsing of the document has begun.
        /// </summary>
        public virtual void ParseDocument(QueryExecutionContext context)
        { }

        /// <summary>
        /// Raised after parsing of the document has ended.
        /// </summary>
        public virtual void ParseDocumentFinished(QueryExecutionContext context)
        { }

        /// <summary>
        /// Raised before document validation has begun.
        /// </summary>
        public virtual void ValidateDocument(QueryExecutionContext context)
        { }

        /// <summary>
        /// Raised after document validation has ended.
        /// </summary>
        public virtual void ValidateDocumentFinished(QueryExecutionContext context)
        { }

        /// <summary>
        /// Raised before document execution has begun.
        /// </summary>
        public virtual void DocumentExecution(ResolverContext context)
        { }

        /// <summary>
        /// Raised after document execution has ended.
        /// </summary>
        public virtual void DocumentExecutionFinished(ResolverContext context)
        { }
    }
}