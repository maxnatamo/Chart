namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when a directive location is invalid.
    /// </summary>
    public class InvalidDirectiveLocationException : Exception
    {
        /// <summary>
        /// The discrepant location name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initialize new InvalidDirectiveLocationException-object.
        /// </summary>
        /// <param name="name">The discrepant name.</param>
        public InvalidDirectiveLocationException(string name)
            : base($"Directive location is not valid: {name}")
        {
            this.Name = name;
        }
    }
}