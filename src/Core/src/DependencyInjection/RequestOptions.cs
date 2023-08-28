namespace Chart.Core
{
    public class RequestOptions
    {
        /// <summary>
        /// Include metrics in the response, in the <c>extensions</c>-field.
        /// </summary>
        public bool EnableMetrics { get; set; } = false;

        /// <summary>
        /// Include exception details, if they occur during executing, in the response.
        /// </summary>
        public bool IncludeExceptions { get; set; } = false;
    }
}