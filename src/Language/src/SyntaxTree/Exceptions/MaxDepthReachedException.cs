namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Should be thrown when the maximum depth of the GraphQL document has been reached.
    /// </summary>
    public class MaxDepthReachedException : Exception
    {
        /// <summary>
        /// The discrepant token.
        /// </summary>
        public int Depth { get; }

        /// <summary>
        /// Initialize new MaxDepthReachedException-object.
        /// </summary>
        /// <param name="depth">The depth reached.</param>
        public MaxDepthReachedException(int depth)
            : base($"The maximum depth for GraphQL-queries has been reached: {depth}")
        {
            this.Depth = depth;
        }
    }
}