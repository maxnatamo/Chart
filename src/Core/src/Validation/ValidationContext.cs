using System.Collections.Concurrent;

namespace Chart.Core
{
    /// <summary>
    /// Context for handling validations.
    /// </summary>
    public abstract class ValidationContext
    {
        /// <inheritdoc cref="Schema" />
        private readonly Schema _schema;

        /// <summary>
        /// The current schema at validation time.
        /// </summary>
        /// <remarks>
        /// If validating during a query, contains the schema which the request is validated against.
        /// </remarks>
        public Schema Schema => this._schema;

        /// <inheritdoc cref="Errors" />
        private ConcurrentBag<Error> _errors { get; set; } = new();

        /// <summary>
        /// List of request errors raised in the current validation cycle.
        /// </summary>
        public IEnumerable<Error> Errors => this._errors;

        public ValidationContext(Schema schema)
        {
            this._schema = schema;
        }

        /// <summary>
        /// Raise a request error onto the context.
        /// </summary>
        public void RaiseError(Error error)
            => this._errors.Add(error);

        internal void ClearErrors()
            => this._errors.Clear();
    }
}