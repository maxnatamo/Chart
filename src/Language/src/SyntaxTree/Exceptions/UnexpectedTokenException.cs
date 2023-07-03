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
    }
}