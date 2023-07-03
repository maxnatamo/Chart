namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when a variable was found, when the expression is constant.
    /// </summary>
    public class VariableNotAllowedException : Exception
    {
        /// <summary>
        /// The discrepant token position.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Initialize new VariableNotAllowedException-object.
        /// </summary>
        /// <param name="position">The discrepant token position.</param>
        public VariableNotAllowedException(int position)
            : base($"Expression must be constant; variables are not allowed in this context: {position}")
        {
            this.Position = position;
        }
    }
}