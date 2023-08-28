namespace Chart.Core.Instrumentation
{
    public interface IServerEventRaiser
    {
        /// <inheritdoc cref="ServerEventListener.RequestReceived" />
        void RequestReceived(QueryExecutionContext context);

        /// <inheritdoc cref="ServerEventListener.RequestEnded" />
        void RequestEnded(QueryExecutionContext context);
    }

    public class ServerEventRaiser : IServerEventRaiser
    {
        private readonly IEnumerable<ServerEventListener> _serverEventListeners;

        public ServerEventRaiser(IEnumerable<ServerEventListener> serverEventListeners)
            => this._serverEventListeners = serverEventListeners;

        /// <inheritdoc />
        public virtual void RequestReceived(QueryExecutionContext context)
            => this.RaiseEvent(e => e.RequestReceived(context));

        /// <inheritdoc />
        public virtual void RequestEnded(QueryExecutionContext context)
            => this.RaiseEvent(e => e.RequestEnded(context));

        private void RaiseEvent(Action<ServerEventListener> action)
        {
            foreach(ServerEventListener listener in this._serverEventListeners)
            {
                action(listener);
            }
        }
    }
}