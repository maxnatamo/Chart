namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when a default value is specified, when none is expected.
    /// </summary>
    public class DefaultValuesNotAllowedException : Exception
    {
        /// <summary>
        /// The discrepant token position.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Initialize new DefaultValuesNotAllowedException-object.
        /// </summary>
        /// <param name="position">The discrepant token position.</param>
        public DefaultValuesNotAllowedException(int position)
            : base($"Default variables are not allowed in this context: {position}")
        {
            this.Position = position;
        }
    }
}