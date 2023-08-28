namespace Chart.Language.SyntaxTree
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
            : base($"Selection set was empty, which is not allowed: line {token.Location.Line}, column {token.Location.Column}")
        {
            this.Token = token;
        }
    }
}