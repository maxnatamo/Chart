using System.Runtime.Serialization;

namespace Chart.Core.Execution
{
    [Serializable]
    public class MiddlewareException : Exception
    {
        public MiddlewareException(string? message = null)
            : base(message)
        { }

        public MiddlewareException(string middlewareName, string? message = null)
            : base(message)
        {
            this.Source = middlewareName;
        }

        protected MiddlewareException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }
    }
}