using System.Collections.ObjectModel;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class QueryExecutionContext
    {
        /// <summary>
        /// The current schema.
        /// </summary>
        public readonly Schema Schema;

        /// <summary>
        /// The query being processed.
        /// </summary>
        public readonly QueryRequest Query;

        /// <summary>
        /// The response for the query.
        /// </summary>
        public readonly ExecutionResult Result;

        /// <summary>
        /// The definition of the operation being executed.
        /// </summary>
        public GraphOperationDefinition Operation { get; internal set; } = default!;

        /// <summary>
        /// A runtime operation, which contains the request fields, after they've been collected.
        /// </summary>
        public GraphOperationDefinition? CollectedOperation { get; } = null;

        /// <summary>
        /// The type definition of the resolved root value to execute.
        /// </summary>
        public ObjectType RootType { get; internal set; } = default!;

        /// <summary>
        /// The graph type of the resolved root value to execute.
        /// </summary>
        public GraphObjectType RootGraphType { get; internal set; } = default!;

        /// <summary>
        /// A resolved value of the root value to execute.
        /// </summary>
        public object RootValue { get; internal set; } = default!;

        /// <summary>
        /// List of all request errors raised.
        /// </summary>
        public readonly List<Error> RequestErrors = new();

        /// <summary>
        /// List of all field errors raised.
        /// </summary>
        public readonly List<Error> FieldErrors = new();

        /// <inheritdoc cref="QueryRequest.Variables" />
        public ReadOnlyDictionary<string, object?>? VariableValues => this.Query.Variables;

        public QueryExecutionContext(Schema schema, QueryRequest query)
        {
            this.Schema = schema;
            this.Query = query;
            this.Result = new ExecutionResult();
        }

        /// <summary>
        /// Raise a request error onto the context.
        /// </summary>
        public void RaiseRequestError(Error error)
            => this.RequestErrors.Add(error);

        /// <summary>
        /// Raise a field error onto the context.
        /// </summary>
        public void RaiseFieldError(Error error)
            => this.FieldErrors.Add(error);
    }
}