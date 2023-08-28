namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when an unexpected token was found.
    /// </summary>
    public class UnexpectedTokenException : Exception
    {
        /// <summary>
        /// The discrepant token position.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Initialize new UnexpectedTokenException-object.
        /// </summary>
        /// <param name="position">The discrepant token position.</param>
        public UnexpectedTokenException(int position)
            : base($"Invalid token at {position}")
        {
            this.Position = position;
        }

        /// <summary>
        /// Initialize new UnexpectedTokenException-object.
        /// </summary>
        /// <param name="token">The discrepant token.</param>
        public UnexpectedTokenException(Token token)
            : base($"Invalid token at line {token.Location.Line}, index {token.Location.Column}. Found {token}")
        {
            this.Position = token.Start;
        }

        /// <summary>
        /// Initialize new UnexpectedTokenException-object.
        /// </summary>
        /// <param name="current">The discrepant token.</param>
        /// <param name="expected">The expected token.</param>
        public UnexpectedTokenException(Token current, TokenType expected)
            : base($"Expected token '{expected}' at line {current.Location.Line}, index {current.Location.Column}. Found {current}")
        {
            this.Position = current.Start;
        }

        /// <summary>
        /// Initialize new UnexpectedTokenException-object.
        /// </summary>
        /// <param name="position">The discrepant token position.</param>
        public UnexpectedTokenException(string token, int position)
            : base($"Invalid token '{token}' at {position}")
        {
            this.Position = position;
        }
    }
}