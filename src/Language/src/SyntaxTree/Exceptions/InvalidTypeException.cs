namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when a type is invalid or unregistered. in the current context.
    /// </summary>
    public class InvalidTypeException : Exception
    {
        /// <summary>
        /// The discrepant name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initialize new InvalidTypeException-object.
        /// </summary>
        /// <param name="name">The discrepant name.</param>
        public InvalidTypeException(string name)
            : base($"Type '{name}' is not registered within the current context")
        {
            this.Name = name;
        }
    }
}