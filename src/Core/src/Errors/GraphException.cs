namespace Chart.Core
{
    [Serializable]
    public class GraphException : Exception
    {
        public override string Message
            => $"{this.Error.Message} {(this.Error.Code is null ? "" : $"({this.Error.Code})")}";

        public Error Error { get; protected set; } = new Error();

        protected GraphException()
        { }

        public GraphException(string message)
            : base(message)
        {
            this.Error = new ErrorBuilder()
                .SetMessage(message)
                .Build();
        }

        public GraphException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.Error = new ErrorBuilder()
                .SetMessage(message)
                .SetException(innerException)
                .Build();
        }
    }
}