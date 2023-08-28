using System.Collections.ObjectModel;

using Chart.Language;
using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public abstract class QueryRequest : DefinitionCollectionBase
    {
        /// <summary>
        /// The actual query.
        /// </summary>
        public abstract string Query { get; }

        /// <summary>
        /// Optional variables to pass along with the query.
        /// </summary>
        public abstract ReadOnlyDictionary<string, object?>? Variables { get; }

        /// <summary>
        /// If multiple operations are defined in the query, defines the one to run.
        /// </summary>
        public abstract string? OperationName { get; }
    }

    internal class DefaultQueryRequest : QueryRequest
    {
        private readonly SchemaParser _schemaParser = new SchemaParser();

        /// <inheritdoc cref="Query" />
        private string _query { get; set; } = string.Empty;

        /// <inheritdoc />
        public override string Query => this._query;

        /// <inheritdoc cref="Variables" />
        private Dictionary<string, object?>? _variables { get; set; } = null;

        /// <inheritdoc />
        public override ReadOnlyDictionary<string, object?>? Variables
        {
            get => this._variables is not null
                ? new(this._variables)
                : null;
        }

        /// <inheritdoc cref="OperationName" />
        private string? _operationName { get; set; } = null;

        /// <inheritdoc />
        public override string? OperationName => this._operationName;

        /// <summary>
        /// Set the actual query of the request.
        /// </summary>
        internal void SetQuery(string query)
            => this._query = query;

        /// <summary>
        /// Add a variable value to the request.
        /// </summary>
        internal void SetVariable(string key, object? value)
        {
            this._variables ??= new Dictionary<string, object?>();
            this._variables.Set(key, value);
        }

        /// <summary>
        /// Unset the variable with the given key in the request.
        /// </summary>
        internal void UnsetVariable(string key)
        {
            this._variables ??= new Dictionary<string, object?>();
            this._variables.Remove(key);
        }

        /// <summary>
        /// Set which operation should be executed, when multiple are given.
        /// </summary>
        internal void SetOperation(string? operation)
            => this._operationName = operation;

        internal void Parse()
        {
            this.Definitions.Clear();

            GraphDocument document = this._schemaParser.ParseQuery(this._query);
            foreach(GraphDefinition definition in document.Definitions)
            {
                this.Definitions.Add(definition);
            }
        }
    }
}