namespace Chart.Core
{
    /// <summary>
    /// Accessor for getting the current execution context.
    /// </summary>
    public interface IQueryExecutionContextAccessor
    {
        /// <summary>
        /// The current schema for the query.
        /// </summary>
        public Schema Schema { get; }

        /// <summary>
        /// The query being resolved.
        /// </summary>
        public QueryRequest Query { get; }

        /// <summary>
        /// Concatenation of the schema and query.
        /// </summary>
        public QueryExecutionContext Context { get; }
    }

    /// <inheritdoc cref="IQueryExecutionContextAccessor" />
    public class QueryExecutionContextAccessor : IQueryExecutionContextAccessor
    {
        /// <inheritdoc />
        public Schema Schema { get; }

        /// <inheritdoc />
        public QueryRequest Query { get; }

        /// <inheritdoc cref="Context" />
        private QueryExecutionContext? context { get; set; } = null;

        /// <inheritdoc />
        public QueryExecutionContext Context
            => this.context ??= new QueryExecutionContext(this.Schema, this.Query);

        public QueryExecutionContextAccessor(SchemaAccessor schemaAccessor, QueryRequestAccessor queryRequestAccessor)
        {
            this.Schema = schemaAccessor.Schema;
            this.Query = queryRequestAccessor.Request;
        }
    }
}