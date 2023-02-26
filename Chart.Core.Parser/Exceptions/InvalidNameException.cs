namespace Chart.Core.Parser
{
    /// <summary>
    /// Should be thrown when a name is invalid in the current context,
    /// such as settings enum-value names to null.
    /// </summary>
    public class InvalidNameException : Exception
    {
        /// <summary>
        /// The discrepant token.
        /// </summary>
        public Token Token { get; }

        /// <summary>
        /// The discrepant name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initialize new InvalidNameException-object.
        /// </summary>
        /// <param name="token">The discrepant token.</param>
        /// <param name="name">The discrepant name.</param>
        public InvalidNameException(Token token, string name)
            : base($"Name '{name}' is not valid within the current context: {token.Start}")
        {
            this.Token = token;
            this.Name = name;
        }
    }
}