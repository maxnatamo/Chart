namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when a name is invalid in the current context,
    /// such as settings enum-value names to null.
    /// </summary>
    public class InvalidNameException : Exception
    {
        /// <summary>
        /// The discrepant token position.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// The discrepant name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initialize new InvalidNameException-object.
        /// </summary>
        /// <param name="position">The discrepant token position.</param>
        /// <param name="name">The discrepant name.</param>
        public InvalidNameException(int position, string name)
            : base($"Name '{name}' is not valid within the current context: {position}")
        {
            this.Position = position;
            this.Name = name;
        }
    }
}