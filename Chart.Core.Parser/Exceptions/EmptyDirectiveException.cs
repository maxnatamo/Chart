namespace Chart.Core.Parser
{
    /// <summary>
    /// Should be thrown when a directive don't have any locations specified.
    /// </summary>
    public class EmptyDirectiveException : Exception
    {
        /// <summary>
        /// The discrepant token.
        /// </summary>
        public Token Token { get; }

        /// <summary>
        /// Initialize new EmptyDirectiveException-object.
        /// </summary>
        /// <param name="token">The discrepant token.</param>
        public EmptyDirectiveException(Token token)
            : base($"Directive locations were not specified, which is required: {token.Start}")
        {
            this.Token = token;
        }
    }
}