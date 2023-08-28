namespace Chart.Core.Instrumentation
{
    public class MetricsFieldExtensionModel
    {
        /// <summary>
        /// The name of the parent type, which the field from resolved from.
        /// </summary>
        public string ParentType { get; set; } = string.Empty;

        /// <summary>
        /// The name of the field.
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// The schema coordinates of the field.
        /// Reference: <seealso href="https://github.com/graphql/graphql-spec/issues/735" />
        /// </summary>
        public string Coordinates => this.ParentType + "." + this.FieldName;

        /// <summary>
        /// The name of the return type.
        /// </summary>
        public string ReturnType { get; set; } = string.Empty;

        /// <summary>
        /// The start time of the request, in milliseconds, relative to the start of the request.
        /// </summary>
        public double StartOffset { get; set; }

        /// <summary>
        /// The amount of time, in milliseconds, the field took to resolve.
        /// </summary>
        public double Duration { get; set; }
    }
}