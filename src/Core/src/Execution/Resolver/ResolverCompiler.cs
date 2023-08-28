using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Chart.Core
{
    /// <summary>
    /// Compiler for creating compiled resolver expressions.
    /// </summary>
    public interface IResolverCompiler
    {
        /// <summary>
        /// Create a resolver for the given member and it's parent object type.
        /// </summary>
        /// <param name="selector">Member selector for selecting which member to create the resolver for.</param>
        /// <param name="resultType">The parent object type for the member.</param>
        /// <returns>The compiled <see cref="FieldResolverDelegate" /> delegate.</returns>
        FieldResolverDelegate CompileResolver<TObject, TMember>(
            Expression<Func<TObject, TMember>> selector,
            Type? resultType = null);

        /// <summary>
        /// Create a resolver for the given member and it's parent object type.
        /// </summary>
        /// <param name="selector">Member selector for selecting which member to create the resolver for.</param>
        /// <param name="resultType">The parent object type for the member.</param>
        /// <returns>The compiled <see cref="FieldResolverDelegate" /> delegate.</returns>
        FieldResolverDelegate CompileResolver<TObject>(
            Expression<Func<TObject, object?>> selector,
            Type? resultType = null);

        /// <inheritdoc cref="IResolverCompiler.CompileResolver{TObject, TRuntime}(Expression{Func{TObject, TRuntime}}, Type?)" />
        /// <param name="member">The member to create the resolver for.</param>
        FieldResolverDelegate CompileResolver(
            MemberInfo member,
            Type? resultType = null);
    }

    /// <inheritdoc />
    public class ResolverCompiler : IResolverCompiler
    {
        /// <summary>
        /// Common parameter expression for all methods, which require an <see cref="IResolverContext" /> instance.
        /// </summary>
        private readonly ParameterExpression _context
            = Expression.Parameter(typeof(IResolverContext), "context");

        /// <summary>
        /// Generic method for retrieving argument values from an <see cref="IResolverContext" /> instance.
        /// </summary>
        private readonly MethodInfo _argument
            = typeof(IResolverContext).GetMethod(nameof(IResolverContext.Argument))!;

        /// <summary>
        /// Generic method for retrieving the result value from an <see cref="IResolverContext" /> instance.
        /// </summary>
        private readonly MethodInfo _parent
            = typeof(IResolverContext).GetMethod(nameof(IResolverContext.Parent))!;

        private readonly ITypeConverter _typeConverter;

        public ResolverCompiler(ITypeConverter typeConverter)
            => this._typeConverter = typeConverter;

        /// <inheritdoc />
        public FieldResolverDelegate CompileResolver<TObject, TRuntime>(
            Expression<Func<TObject, TRuntime>> resolver,
            Type? parentType = null) =>
            this.CompileResolver<TObject>(
                Expression.Lambda<Func<TObject, object?>>(
                    Expression.Convert(
                        resolver,
                        typeof(object))),
                parentType);

        public FieldResolverDelegate CompileResolver<TObject>(
            Expression<Func<TObject, object?>> resolver,
            Type? parentType = null) =>
            resolver switch
            {
                { Body: MemberExpression memberExpression }
                    => this.CompileResolver(memberExpression.Member, parentType),

                { Body: MethodCallExpression methodCallExpression }
                    => this.CompileResolver(methodCallExpression.Method, parentType),

                _ => throw new NotSupportedException("Only members and method-calls can be resolved.")
            };

        /// <inheritdoc />
        public FieldResolverDelegate CompileResolver(
            MemberInfo member,
            Type? parentType = null)
        {
            parentType = member.ReflectedType ?? member.DeclaringType!;

            return member switch
            {
                FieldInfo field => this.CreateResolver(field, parentType),
                PropertyInfo property => this.CreateResolver(property, parentType),
                MethodInfo method => this.CreateResolver(method, parentType),

                _ => throw new NotSupportedException("Only fields, properties and methods can be compiled for resolvers.")
            };
        }

        /// <summary>
        /// Create a resolver for the given field and it's parent object type.
        /// </summary>
        /// <param name="field">The field to create the resolver for.</param>
        /// <param name="parentType">The parent object type for the field.</param>
        /// <returns>The compiled <see cref="FieldResolverDelegate" /> delegate.</returns>
        private FieldResolverDelegate CreateResolver(
            FieldInfo field,
            Type parentType)
        {
            MethodCallExpression owner = Expression.Call(this._context, this._parent.MakeGenericMethod(parentType));
            Expression memberResolver = Expression.Convert(
                Expression.Field(owner, field),
                typeof(object));

            return Expression.Lambda<FieldResolverDelegate>(memberResolver, this._context).Compile();
        }

        /// <summary>
        /// Create a resolver for the given property and it's parent object type.
        /// </summary>
        /// <param name="property">The property to create the resolver for.</param>
        /// <param name="parentType">The parent object type for the property.</param>
        /// <returns>The compiled <see cref="FieldResolverDelegate" /> delegate.</returns>
        private FieldResolverDelegate CreateResolver(
            PropertyInfo property,
            Type parentType)
        {
            MethodCallExpression owner = Expression.Call(this._context, this._parent.MakeGenericMethod(parentType));
            Expression memberResolver = Expression.Convert(
                Expression.Property(owner, property),
                typeof(object));

            return Expression.Lambda<FieldResolverDelegate>(memberResolver, this._context).Compile();
        }

        /// <summary>
        /// Create a resolver for the given method and it's parent object type.
        /// </summary>
        /// <param name="method">The method to create the resolver for.</param>
        /// <param name="parentType">The parent object type for the method.</param>
        /// <returns>The compiled <see cref="FieldResolverDelegate" /> delegate.</returns>
        private FieldResolverDelegate CreateResolver(
            MethodInfo method,
            Type parentType)
        {
            ParameterInfo[] argumentMap = method.GetParameters();

            // argNames
            ParameterExpression argumentNamesParameter = Expression.Parameter(typeof(string[]), "argNames");
            Expression[] argumentMethodExpressions = new Expression[argumentMap.Length];

            this.CreateMethodParameterExpressions(
                argumentMap,
                argumentNamesParameter,
                argumentMethodExpressions);

            // context.Parent<TObj>()
            MethodCallExpression parentMethodExpression =
                Expression.Call(
                    instance: this._context,
                    method: this._parent.MakeGenericMethod(parentType));

            // context.Parent<TObj>().Method(ctx.Argument<TArg>(argNames[i]))
            MethodCallExpression targetMethodExpression =
                Expression.Call(
                    instance: parentMethodExpression,
                    method: method,
                    arguments: argumentMethodExpressions);

            // ctx => context.Parent<TObj>().Method(ctx.Argument<TArg>(argNames[i]))
            Func<IResolverContext, string[], object?> expression =
                Expression.Lambda<Func<IResolverContext, string[], object?>>(
                    targetMethodExpression,
                    this._context,
                    argumentNamesParameter).Compile();

            string[] argumentNames = argumentMap.Select(v => v.Name!).ToArray();

            return new FieldResolverDelegate(ctx => expression(ctx, argumentNames));
        }

        /// <summary>
        /// Validate the given method parameters against the given argument map.
        /// </summary>
        /// <param name="parameters">The parameters to validate.</param>
        /// <param name="arguments">Unordered map with argument names and values.</param>
        /// <returns>The given parameters.</returns>
        internal ParameterInfo[] CreateMethodParameters(
            ParameterInfo[] parameters,
            ReadOnlyDictionary<string, object?> arguments)
        {
            ParameterInfo[] parameterInfos = new ParameterInfo[parameters.Length];

            if(arguments.Count > parameterInfos.Length)
            {
                throw new ArgumentException($"Argument count is greather than the amount of parameters ({arguments.Count} > {parameterInfos.Length})");
            }

            for(int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];

                if(!arguments.TryGetValue(parameter.Name!, out object? value) && !parameter.HasDefaultValue)
                {
                    throw new ArgumentException($"Argument '{parameter.Name}' not found.");
                }

                if(value is null && !parameter.ParameterType.IsNullableType())
                {
                    throw new ArgumentException($"Argument value cannot be null: {parameter.Name}");
                }

                if(value is not null && !this._typeConverter.TryConvertValue(value.GetType(), parameter.ParameterType, value, out _))
                {
                    throw new ArgumentException($"Argument value '{value.GetType().Name}' is not assignable to '{parameter.ParameterType.Name}'");
                }

                parameterInfos[i] = parameter;
            }

            return parameterInfos;
        }

        /// <inheritdoc cref="ResolverCompiler.CreateMethodParameters(ParameterInfo[], ReadOnlyDictionary{string, object?})" />
        /// <param name="method">The method to validate parameters for.</param>
        internal ParameterInfo[] CreateMethodParameters(
            MethodInfo method,
            ReadOnlyDictionary<string, object?> arguments)
            => this.CreateMethodParameters(method.GetParameters(), arguments);

        /// <summary>
        /// Populate a list of expressions with new <see cref="MethodCallExpression" />,
        /// which are used for method resolvers.
        /// </summary>
        /// <param name="argumentMap">Ordered array of parameters, for the corresponding method.</param>
        /// <param name="argumentNamesParameter">Parameter expression which declares the <c>argumentNames</c> parameter.</param>
        /// <param name="argumentExpressions">The array to populate.</param>
        private void CreateMethodParameterExpressions(
            ParameterInfo[] argumentMap,
            ParameterExpression argumentNamesParameter,
            Expression[] argumentExpressions)
        {
            if(argumentExpressions.Length != argumentMap.Length)
            {
                throw new InvalidDataException($"Failed to allocate argument expression array ({argumentMap.Length} != {argumentExpressions.Length})");
            }

            for(int i = 0; i < argumentMap.Length; i++)
            {
                ParameterInfo parameterInfo = argumentMap[i];

                // argNames[i]
                BinaryExpression argumentIndexExpression = Expression.ArrayIndex(
                        argumentNamesParameter,
                        Expression.Constant(i));

                // ctx.Argument<TArg>(argNames[i])
                MethodCallExpression argumentMethodExpression =
                    Expression.Call(
                        instance: this._context,
                        method: this._argument.MakeGenericMethod(parameterInfo.ParameterType),
                        arguments: argumentIndexExpression);

                argumentExpressions[i] = argumentMethodExpression;
            }
        }
    }
}