namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when a brace doesn't have a matching brace elsewhere.
    /// </summary>
    public class UnmatchedBraceException : Exception
    {
        /// <summary>
        /// The discrepant token position.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Initialize new UnmatchedBraceException-object.
        /// </summary>
        /// <param name="position">The discrepant token position.</param>
        public UnmatchedBraceException(int position)
            : base($"Unmatched brace found at {position}")
        {
            this.Position = position;
        }
    }
}