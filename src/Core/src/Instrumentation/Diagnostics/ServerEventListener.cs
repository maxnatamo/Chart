namespace Chart.Core.Instrumentation
{
    /// <summary>
    /// Event listener logic to track server diagnostics.
    /// </summary>
    public abstract class ServerEventListener : IEventListener
    {
        /// <summary>
        /// Raised just after a new request has been received.
        /// </summary>
        public virtual void RequestReceived(QueryExecutionContext context)
        { }

        /// <summary>
        /// Raised just before a response for a request has been returned.
        /// </summary>
        public virtual void RequestEnded(QueryExecutionContext context)
        { }
    }
}