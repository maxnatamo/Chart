namespace Chart.Core.Parser
{
    /// <summary>
    /// Should be thrown when an unexpected token was found.
    /// </summary>
    public class UnexpectedTokenException : Exception
    {
        /// <summary>
        /// The discrepant token.
        /// </summary>
        public Token Token { get; }

        /// <summary>
        /// Initialize new UnexpectedTokenException-object.
        /// </summary>
        /// <param name="token">The discrepant token.</param>
        public UnexpectedTokenException(Token token)
            : base($"Invalid token at {token.Start}, {token.ToString()}")
        {
            this.Token = token;
        }
    }
}