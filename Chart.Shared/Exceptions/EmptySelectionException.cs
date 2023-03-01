namespace Chart.Shared.Exceptions
{
    public class EmptySelectionException : Exception
    {
        /// <summary>
        /// The discrepant token position.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Initialize new EmptySelectionException-object.
        /// </summary>
        /// <param name="position">The discrepant token position.</param>
        public EmptySelectionException(int position)
            : base($"Selection set was empty, which is not allowed: {position}")
        {
            this.Position = position;
        }
    }
}