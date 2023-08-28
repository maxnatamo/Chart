namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when a directive don't have any locations specified.
    /// </summary>
    public class EmptyDirectiveException : Exception
    {
        /// <summary>
        /// The discrepant token position.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Initialize new EmptyDirectiveException-object.
        /// </summary>
        /// <param name="position">The discrepant token position.</param>
        public EmptyDirectiveException(int position)
            : base($"Directive locations were not specified, which is required: {position}")
        {
            this.Position = position;
        }
    }
}