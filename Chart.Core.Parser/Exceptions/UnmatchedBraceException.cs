namespace Chart.Core.Parser
{
    /// <summary>
    /// Should be thrown when a brace doesn't have a matching brace elsewhere.
    /// </summary>
    public class UnmatchedBraceException : Exception
    {
        /// <summary>
        /// The discrepant token.
        /// </summary>
        public Token Token { get; }

        /// <summary>
        /// Initialize new UnmatchedBraceException-object.
        /// </summary>
        /// <param name="token">The unmatched token.</param>
        public UnmatchedBraceException(Token token)
            : base($"Unmatched brace found at {token.Start}")
        {
            this.Token = token;
        }
    }
}