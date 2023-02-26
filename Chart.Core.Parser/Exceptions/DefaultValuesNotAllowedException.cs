namespace Chart.Core.Parser
{
    /// <summary>
    /// Should be thrown when a default value is specified, when none is expected.
    /// </summary>
    public class DefaultValuesNotAllowedException : Exception
    {
        /// <summary>
        /// The discrepant token.
        /// </summary>
        public Token Token { get; }

        /// <summary>
        /// Initialize new DefaultValuesNotAllowedException-object.
        /// </summary>
        /// <param name="token">The discrepant token.</param>
        public DefaultValuesNotAllowedException(Token token)
            : base($"Default variables are not allowed in this context: {token.Start}")
        {
            this.Token = token;
        }
    }
}