using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Executor for fields-selections and field selection-sets.
    /// </summary>
    public interface IFieldExecutor
    {
        /// <summary>
        /// Execute the given selection set on the current execution context.
        /// </summary>
        /// <param name="context">The current execution context.</param>
        /// <param name="selection">The selection to execute.</param>
        /// <param name="cancellationToken">Token for cancelling the execution.</param>
        /// <returns>The resolved value.</returns>
        ValueTask<object?> ExecuteSelectionAsync(
            QueryExecutionContext context,
            GraphFieldSelection selection,
            CancellationToken cancellationToken);

        /// <inheritdoc cref="ExecuteSelectionAsync(QueryExecutionContext, GraphFieldSelection, CancellationToken)" />
        /// <param name="objectType">The type of the operation being executed.</param>
        /// <param name="objectValue">The object instance of the operation.</param>
        /// <returns>The resolved value, derived from <paramref name="objectValue" />.</returns>
        ValueTask<object?> ExecuteSelectionAsync(
            GraphFieldSelection selection,
            GraphObjectType objectType,
            object? objectValue,
            CancellationToken cancellationToken);

        /// <inheritdoc cref="ExecuteSelectionAsync(GraphFieldSelection, GraphObjectType, object?, CancellationToken)" />
        ValueTask<object?> ExecuteSelectionAsync(
            QueryExecutionContext context,
            GraphFieldSelection selection,
            GraphObjectType objectType,
            object? objectValue,
            CancellationToken cancellationToken);
    }

    public partial class FieldExecutor : IFieldExecutor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFieldResolver _fieldResolver;
        private readonly ITypeResolver _typeResolver;
        private readonly IValueCoercer _valueCoercer;
        private readonly IValueRegistry _valueRegistry;
        private readonly IQueryExecutionContextAccessor _queryExecutionContextAccessor;

        public FieldExecutor(
            IServiceProvider serviceProvider,
            IFieldResolver fieldResolver,
            ITypeResolver typeResolver,
            IValueCoercer valueCoercer,
            IValueRegistry valueRegistry,
            IQueryExecutionContextAccessor queryExecutionContextAccessor)
        {
            this._serviceProvider = serviceProvider;
            this._fieldResolver = fieldResolver;
            this._typeResolver = typeResolver;
            this._valueCoercer = valueCoercer;
            this._valueRegistry = valueRegistry;
            this._queryExecutionContextAccessor = queryExecutionContextAccessor;
        }

        /// <inheritdoc />
        public async ValueTask<object?> ExecuteSelectionAsync(
            QueryExecutionContext context,
            GraphFieldSelection selection,
            CancellationToken cancellationToken) =>
            await this.ExecuteSelectionAsync(
                this._queryExecutionContextAccessor.Context,
                selection,
                context.RootGraphType,
                context.RootValue,
                cancellationToken);

        /// <inheritdoc />
        public async ValueTask<object?> ExecuteSelectionAsync(
            GraphFieldSelection selection,
            GraphObjectType objectType,
            object? objectValue,
            CancellationToken cancellationToken) =>
            await this.ExecuteSelectionAsync(
                this._queryExecutionContextAccessor.Context,
                selection,
                objectType,
                objectValue,
                cancellationToken);

        /// <inheritdoc />
        public async ValueTask<object?> ExecuteSelectionAsync(
            QueryExecutionContext context,
            GraphFieldSelection selection,
            GraphObjectType objectType,
            object? objectValue,
            CancellationToken cancellationToken) =>
            await this.ExecuteFieldAsync(
                context,
                objectType,
                objectValue,
                selection,
                new Stack<object>(),
                cancellationToken);

        private async ValueTask<Dictionary<string, object?>> ExecuteSelectionsAsync(
            QueryExecutionContext context,
            List<GraphFieldSelection> selections,
            GraphObjectType objectType,
            object? objectValue,
            Stack<object> path,
            CancellationToken cancellationToken)
        {
            Dictionary<string, object?> response = new();

            foreach(GraphFieldSelection fieldSelection in selections)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string responseKey = fieldSelection.Alias ?? fieldSelection.Name;

                path.Push(responseKey);

                response[responseKey] = await this.ExecuteFieldAsync(
                    context,
                    objectType,
                    objectValue,
                    fieldSelection,
                    path,
                    cancellationToken
                );

                path.Pop();
            }

            return response;
        }

        private async ValueTask<object?> ExecuteFieldAsync(
            QueryExecutionContext context,
            GraphObjectType objectType,
            object? objectValue,
            GraphFieldSelection selection,
            Stack<object> path,
            CancellationToken cancellationToken)
        {
            GraphField? field = (objectType.Fields?.Fields.FirstOrDefault(v => v.Name == selection.Name))
                ?? throw new NotImplementedException();

            Dictionary<string, object?>? argumentValues = this.CoerceArgumentValues(context, field, selection);

            object? resolvedValue = await this._fieldResolver.ResolveAsync(
               context,
               selection,
               objectType,
               objectValue,
               argumentValues,
               cancellationToken);

            resolvedValue = await this.CompleteValueAsync(
                context,
                field.Type,
                selection,
                resolvedValue,
                path,
                cancellationToken);

            return resolvedValue;
        }
    }
}