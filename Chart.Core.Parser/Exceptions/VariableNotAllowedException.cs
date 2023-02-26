namespace Chart.Core.Parser
{
    /// <summary>
    /// Should be thrown when a variable was found, when the expression is constant.
    /// </summary>
    public class VariableNotAllowedException : Exception
    {
        /// <summary>
        /// The discrepant token.
        /// </summary>
        public Token Token { get; }

        /// <summary>
        /// Initialize new VariableNotAllowedException-object.
        /// </summary>
        /// <param name="token">The discrepant token.</param>
        public VariableNotAllowedException(Token token)
            : base($"Expression must be constant; variables are not allowed in this context: {token.Start}")
        {
            this.Token = token;
        }
    }
}