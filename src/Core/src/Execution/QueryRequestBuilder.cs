namespace Chart.Core
{
    public class QueryRequestBuilder
    {
        private readonly DefaultQueryRequest _queryRequest;

        public QueryRequestBuilder()
        {
            this._queryRequest = new DefaultQueryRequest();
        }

        /// <inheritdoc cref="DefaultQueryRequest.SetQuery(string)" />
        public QueryRequestBuilder SetQuery(string query)
        {
            this._queryRequest.SetQuery(query);
            return this;
        }

        /// <inheritdoc cref="DefaultQueryRequest.SetVariable(string, object?)" />
        public QueryRequestBuilder SetVariable(string key, object? value)
        {
            this._queryRequest.SetVariable(key, value);
            return this;
        }

        /// <inheritdoc cref="DefaultQueryRequest.UnsetVariable(string)" />
        public QueryRequestBuilder UnsetVariable(string key)
        {
            this._queryRequest.UnsetVariable(key);
            return this;
        }

        /// <inheritdoc cref="DefaultQueryRequest.SetOperation(string)" />
        public QueryRequestBuilder SetOperation(string? operation)
        {
            this._queryRequest.SetOperation(operation);
            return this;
        }

        /// <summary>
        /// Create the final query request.
        /// </summary>
        /// <param name="parse">Whether to parse the request into a document.</param>
        public QueryRequest Create(bool parse = false)
        {
            if(parse)
            {
                this._queryRequest.Parse();
            }

            return this._queryRequest;
        }
    }
}