namespace Chart.Core
{
    /// <summary>
    /// Options for the execution environment for requests.
    /// </summary>
    public class GraphOptions
    {
        /// <summary>
        /// Options relating to requests.
        /// </summary>
        public RequestOptions Request { get; set; } = new();

        /// <summary>
        /// Options relating to validation.
        /// </summary>
        public ValidationOptions Validation { get; set; } = new();
    }
}