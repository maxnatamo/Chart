namespace Chart.Core.Instrumentation
{
    public class MetricsExtensionModel
    {
        public class ParsingModel
        {
            /// <summary>
            /// The start time of the parsing phase, in milliseconds, relative to the start of the request.
            /// </summary>
            public double StartOffset { get; set; }

            /// <summary>
            /// The amount of time, in milliseconds, the parsing phase took.
            /// </summary>
            public double Duration { get; set; }
        }

        public class ValidationModel
        {
            /// <summary>
            /// The start time of the validation phase, in milliseconds, relative to the start of the request.
            /// </summary>
            public double StartOffset { get; set; }

            /// <summary>
            /// The amount of time, in milliseconds, the validation phase took.
            /// </summary>
            public double Duration { get; set; }
        }

        public class ExecutionModel
        {
            /// <summary>
            /// The start time of the execution phase, in milliseconds, relative to the start of the request.
            /// </summary>
            public double StartOffset { get; set; }

            /// <summary>
            /// The amount of time, in milliseconds, the execution phase took.
            /// </summary>
            public double Duration { get; set; }

            /// <summary>
            /// Precise timings of each resolver.
            /// </summary>
            public List<MetricsFieldExtensionModel> Resolvers { get; set; } = new();
        }

        /// <summary>
        /// The start time of the request, in UTC.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The end time of the request, in UTC.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// The amount of time, in milliseconds, the entire request took to
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Metrics related to parsing.
        /// </summary>
        public ParsingModel Parsing { get; set; } = new();

        /// <summary>
        /// Metrics related to valiation.
        /// </summary>
        public ValidationModel Validation { get; set; } = new();

        /// <summary>
        /// Metrics related to execution.
        /// </summary>
        public ExecutionModel Execution { get; set; } = new();
    }
}