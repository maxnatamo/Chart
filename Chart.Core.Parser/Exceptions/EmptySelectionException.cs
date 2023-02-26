namespace Chart.Core.Parser
{
    public class EmptySelectionException : Exception
    {
        /// <summary>
        /// The discrepant token.
        /// </summary>
        public Token Token { get; }

        /// <summary>
        /// Initialize new EmptySelectionException-object.
        /// </summary>
        /// <param name="token">The discrepant token.</param>
        public EmptySelectionException(Token token)
            : base($"Selection set was empty, which is not allowed: {token.Start}")
        {
            this.Token = token;
        }
    }
}