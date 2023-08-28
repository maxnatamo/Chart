using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Context for validating requests against the current schema.
    /// </summary>
    public class DocumentValidationContext : ValidationContext
    {
        /// <inheritdoc cref="Query" />
        private readonly QueryRequest _query;

        /// <summary>
        /// The query to be validated.
        /// </summary>
        public QueryRequest Query => this._query;

        public DocumentValidationContext(Schema schema, QueryRequest query)
            : base(schema)
        {
            this._query = query;
        }
    }
}