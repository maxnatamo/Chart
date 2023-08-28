using System.Reflection;

using Chart.Core.Execution;
using Chart.Core.Instrumentation;
using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public interface IFieldResolver
    {
        /// <summary>
        /// Resolve the value of a field on the given object type.
        /// </summary>
        /// <param name="context">The current execution context.</param>
        /// <param name="selection">The corresponding field selection to resolve for.</param>
        /// <param name="objectType">The type definition being resolved against.</param>
        /// <param name="objectValue">The object value to retrieve the value from.</param>
        /// <param name="arguments">Any optional arguments for the field, if any.</param>
        /// <returns>The resolved value, which may be <see langword="null" />.</returns>
        ValueTask<object?> ResolveAsync(
            QueryExecutionContext context,
            GraphFieldSelection selection,
            GraphObjectType objectType,
            object? objectValue,
            Dictionary<string, object?>? arguments,
            CancellationToken cancellationToken);
    }

    public class FieldResolver : IFieldResolver
    {
        private readonly ITypeResolver _typeResolver;
        private readonly ITypeConverter _typeConverter;
        private readonly IServiceProvider _serviceProvider;
        private readonly IExecutionEventRaiser _executionEventRaiser;

        private int FieldMiddlewareIndex = 0;
        private readonly List<IFieldExecutionMiddleware> _fieldMiddlewares;

        public FieldResolver(
            ITypeResolver typeResolver,
            ITypeConverter typeConverter,
            IServiceProvider serviceProvider,
            IExecutionEventRaiser executionEventRaiser,
            IEnumerable<IFieldExecutionMiddleware> fieldMiddlewares)
        {
            this._typeResolver = typeResolver;
            this._typeConverter = typeConverter;
            this._serviceProvider = serviceProvider;
            this._executionEventRaiser = executionEventRaiser;
            this._fieldMiddlewares = fieldMiddlewares.ToList();
        }

        public async ValueTask<object?> ResolveAsync(
            QueryExecutionContext context,
            GraphFieldSelection selection,
            GraphObjectType objectType,
            object? objectValue,
            Dictionary<string, object?>? arguments,
            CancellationToken cancellationToken)
        {
            if(objectValue is null && cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            if(!this._typeResolver.TryResolveTypeDefinition(objectType.Name, out ObjectType? objectTypeDefinition))
            {
                throw new NotImplementedException(objectType.Name);
            }

            FieldDefinition? fieldDefinition = objectTypeDefinition.Fields
                .FirstOrDefault(v => v.Name == selection.Name) ?? throw new NotImplementedException(objectType.Name);

            if(fieldDefinition.Resolver is null)
            {
                throw new NotImplementedException(objectType.Name);
            }

            if(fieldDefinition.Member is MethodInfo methodInfo)
            {
                arguments ??= new Dictionary<string, object?>();
                if(!this.ValidateArguments(context, methodInfo, arguments, selection.Location!))
                {
                    return null;
                }
            }

            this.FieldMiddlewareIndex = 0;

            ResolverContext resolverContext = new(
                context,
                objectType,
                objectValue,
                fieldDefinition,
                selection,
                arguments,
                this._serviceProvider);

            this._executionEventRaiser.DocumentExecution(resolverContext);

            object? result = await this.InvokeMiddlewareAsync(
                resolverContext,
                cancellationToken);

            this._executionEventRaiser.DocumentExecutionFinished(resolverContext);

            return result;
        }

        internal bool ValidateArguments(
            QueryExecutionContext context,
            MethodInfo method,
            Dictionary<string, object?> arguments,
            GraphLocation location)
        {
            ParameterInfo[] parameters = method.GetParameters();

            if(arguments.Count > parameters.Length)
            {
                foreach(KeyValuePair<string, object?> argument in arguments)
                {
                    if(parameters.Any(v => v.Name == argument.Key))
                    {
                        continue;
                    }

                    context.RaiseRequestError(
                        DefaultErrors.ArgumentNotFound(
                            argument.Key,
                            method)
                        .WithLocation(location));
                }
                return false;
            }

            foreach(ParameterInfo parameter in parameters)
            {
                string parameterName = parameter.Name!;

                if(!arguments.TryGetValue(parameterName, out object? argumentValue))
                {
                    if(parameter.HasDefaultValue)
                    {
                        arguments[parameterName] = parameter.DefaultValue;
                        continue;
                    }

                    context.RaiseRequestError(
                        DefaultErrors.ArgumentNotFound(
                            parameterName,
                            method));

                    return false;
                }

                if(argumentValue is not null)
                {
                    if(!this._typeConverter.TryConvertValue(
                        argumentValue.GetType(),
                        parameter.ParameterType,
                        argumentValue, out object?
                        convertedArgumentValue))
                    {
                        context.RaiseRequestError(
                            DefaultErrors.InvalidArgumentValue(
                                method.Name,
                                parameterName,
                                argumentValue.GetType(),
                                parameter.ParameterType));

                        return false;
                    }

                    arguments[parameterName] = convertedArgumentValue;
                }
            }

            return true;
        }

        private async ValueTask<object?> InvokeMiddlewareAsync(ResolverContext resolverContext, CancellationToken cancellationToken)
        {
            if(this._fieldMiddlewares.Count <= this.FieldMiddlewareIndex)
            {
                string? middlewareName = this._fieldMiddlewares.LastOrDefault()?.GetType().Name;

                throw new MiddlewareException(
                    middlewareName ?? "(null)",
                    "Last middleware failed to resolve a value.");
            }

            IFieldExecutionMiddleware middleware = this._fieldMiddlewares[this.FieldMiddlewareIndex++];

            return await middleware.InvokeAsync(
                resolverContext,
                this.InvokeMiddlewareAsync,
                cancellationToken);
        }
    }
}