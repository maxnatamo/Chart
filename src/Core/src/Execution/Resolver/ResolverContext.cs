using System.Collections.ObjectModel;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public interface IResolverContext
    {
        /// <summary>
        /// The current schema.
        /// </summary>
        Schema Schema { get; }

        /// <summary>
        /// The query being processed.
        /// </summary>
        QueryRequest Query { get; }

        /// <summary>
        /// The schema type of the current object being resolved.
        /// </summary>
        GraphObjectType ObjectType { get; }

        /// <summary>
        /// Global service provider.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// The name of the field in the response.
        /// </summary>
        string ResponseKey { get; }

        /// <summary>
        /// Get all arguments passed to the field.
        /// </summary>
        /// <returns>Unordered map of all the arguments passed to the field.</returns>
        ReadOnlyDictionary<string, object?> Arguments();

        /// <summary>
        /// Retrieve an argument from the field, with the given name.
        /// </summary>
        /// <returns>The value of the argument.</returns>
        /// <exception cref="KeyNotFoundException">No argument with the given name was found.</exception>
        /// <exception cref="NullReferenceException">The argument was found, but it was <see langword="null" />.</exception>
        TArgument Argument<TArgument>(string argumentName);

        /// <summary>
        /// Retrieve an optional argument from the field, with the given name.
        /// </summary>
        /// <returns>
        /// The value of the argument, if found and non-null.
        /// Otherwise, returns empty argument.
        /// </returns>
        Optional<TArgument> ArgumentOptional<TArgument>(string argumentName);

        /// <summary>
        /// Retrieve a variable from the query, with the given name.
        /// </summary>
        /// <returns>The value of the variable.</returns>
        /// <exception cref="KeyNotFoundException">No variable with the given name was found.</exception>
        /// <exception cref="NullReferenceException">The variable was found, but it was <see langword="null" />.</exception>
        TVariable Variable<TVariable>(string variableName);

        /// <summary>
        /// Retrieve an optional variable from the field, with the given name.
        /// </summary>
        /// <returns>
        /// The value of the variable, if found and non-null.
        /// Otherwise, returns empty variable.
        /// </returns>
        Optional<TVariable> VariableOptional<TVariable>(string variableName);

        /// <summary>
        /// Raise a non-terminating field error to the parent context.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        void RaiseError(string errorMessage);

        /// <summary>
        /// Raise a non-terminating field error to the parent context.
        /// </summary>
        /// <param name="error">The error to raise.</param>
        void RaiseError(Error error);

        /// <summary>
        /// Get the result of the parent resolver.
        /// </summary>
        TResult? Parent<TResult>();
    }

    public class ResolverContext : IResolverContext
    {
        /// <summary>
        /// The current execution context.
        /// </summary>
        public readonly QueryExecutionContext ExecutionContext;

        /// <summary>
        /// Unordered map of all arguments passed to the field.
        /// </summary>
        private readonly Dictionary<string, object?> _arguments;

        /// <summary>
        /// The value being resolved.
        /// </summary>
        private readonly object? _objectValue;

        /// <summary>
        /// The field being resolved.
        /// </summary>
        public readonly FieldDefinition Field;

        /// <summary>
        /// The field selection to resolve.
        /// </summary>
        public readonly GraphFieldSelection Selection;

        /// <inheritdoc />
        public Schema Schema => this.ExecutionContext.Schema;

        /// <inheritdoc />
        public QueryRequest Query => this.ExecutionContext.Query;

        /// <inheritdoc />
        public GraphObjectType ObjectType { get; }

        /// <inheritdoc />
        public IServiceProvider Services { get; }

        /// <inheritdoc />
        public string ResponseKey { get; }

        public ResolverContext(
            QueryExecutionContext context,
            GraphObjectType objectType,
            object? objectValue,
            FieldDefinition field,
            GraphFieldSelection selection,
            Dictionary<string, object?>? arguments,
            IServiceProvider serviceProvider)
        {
            this.ExecutionContext = context;
            this._arguments = arguments ?? new Dictionary<string, object?>();
            this.ObjectType = objectType;
            this._objectValue = objectValue;
            this.Field = field;
            this.Selection = selection;
            this.Services = serviceProvider;

            this.ResponseKey = this.Selection.Alias ?? this.Selection.Name;
        }

        /// <inheritdoc />
        public ReadOnlyDictionary<string, object?> Arguments()
            => new(this._arguments);

        /// <inheritdoc />
        public TArgument Argument<TArgument>(string argumentName)
        {
            if(!this._arguments.TryGetValue(argumentName, out object? value))
            {
                throw new KeyNotFoundException($"No argument with name '{argumentName}' was found on the field '{this.Selection.Name}'.");
            }

            if(value is null)
            {
                throw new NullReferenceException($"Argument value '{argumentName}' on the field '{this.Selection.Name}' was null.");
            }

            if(value is not TArgument argumentValue)
            {
                throw new NullReferenceException($"Argument value '{argumentName}' on the field '{this.Selection.Name}' is not of type '{typeof(TArgument).Name}'.");
            }

            return argumentValue;
        }

        /// <inheritdoc />
        public Optional<TArgument> ArgumentOptional<TArgument>(string argumentName)
        {
            if(!this._arguments.TryGetValue(argumentName, out object? value))
            {
                return new Optional<TArgument>(default, false);
            }

            if(value is null)
            {
                return new Optional<TArgument>(default, false);
            }

            return (TArgument) value;
        }

        /// <inheritdoc />
        public TVariable Variable<TVariable>(string variableName)
        {
            if(this.ExecutionContext.VariableValues is null || !this.ExecutionContext.VariableValues.TryGetValue(variableName, out object? value))
            {
                throw new KeyNotFoundException($"No varible with name '{variableName}' was found in the query.");
            }

            if(value is null)
            {
                throw new NullReferenceException($"Variable value '{variableName}' was null.");
            }

            if(value is not TVariable variableValue)
            {
                throw new NullReferenceException($"Variable value '{variableName}' is not of type '{typeof(TVariable).Name}'.");
            }

            return variableValue;
        }

        /// <inheritdoc />
        public Optional<TVariable> VariableOptional<TVariable>(string variableName)
        {
            if(this.ExecutionContext.VariableValues is null || !this.ExecutionContext.VariableValues.TryGetValue(variableName, out object? value))
            {
                return new Optional<TVariable>(default, false);
            }

            if(value is null)
            {
                return new Optional<TVariable>(default, false);
            }

            return (TVariable) value;
        }

        /// <inheritdoc />
        public void RaiseError(string errorMessage) =>
            this.RaiseError(new ErrorBuilder()
                .SetMessage(errorMessage)
                .Build());

        /// <inheritdoc />
        public void RaiseError(Error error)
            => this.ExecutionContext.RaiseFieldError(error);

        /// <inheritdoc />
        public TResult? Parent<TResult>()
            => (TResult?) this._objectValue;
    }
}