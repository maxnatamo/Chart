using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Chart.Core
{
    /// <summary>
    /// Cache for compiled <see cref="FieldResolverDelegate" /> resolver delegates.
    /// </summary>
    public interface IResolverCache
    {
        /// <summary>
        /// The cached <see cref="FieldResolverDelegate" /> corresponding to the given member.
        /// </summary>
        /// <param name="member">The member key to retrieve the resolver from.</param>
        /// <returns>The <see cref="FieldResolverDelegate" /> delegate, if found. Otherwise, <see langword="null" />.</returns>
        FieldResolverDelegate? Get(MemberInfo member);

        /// <inheritdoc cref="IResolverCache.Get(MemberInfo)" />
        /// <param name="selector">Expression for selecting which member to retrieve the resolver from.</param>
        FieldResolverDelegate? Get<TObject>(
            Expression<Func<TObject, object?>> selector);

        /// <inheritdoc cref="IResolverCache.Get{TObject}(Expression{Func{TObject, object?}})" />
        FieldResolverDelegate? Get<TObject, TMember>(
            Expression<Func<TObject, TMember>> selector);

        /// <summary>
        /// Compile a new <see cref="FieldResolverDelegate" /> delegate for the given member.
        /// </summary>
        /// <param name="member">The member to create the resolver for.</param>
        /// <param name="resultType">The parent type of the member.</param>
        /// <param name="cache">Whether to store the resulting delegate in the cache.</param>
        /// <returns>The compiled <see cref="FieldResolverDelegate" /> delegate.</returns>
        FieldResolverDelegate Compile(
            MemberInfo member,
            Type? resultType = null,
            bool cache = true);

        /// <inheritdoc cref="IResolverCache.Compile(MemberInfo, Type?, bool)" />
        /// <param name="selector">Expression for selecting which member to compile the resolver from.</param>
        FieldResolverDelegate Compile<TObject>(
            Expression<Func<TObject, object?>> selector,
            bool cache = true);

        /// <inheritdoc cref="IResolverCache.Compile{TObject}(Expression{Func{TObject, object?}}, bool)" />
        FieldResolverDelegate Compile<TObject, TMember>(
            Expression<Func<TObject, TMember>> selector,
            bool cache = true);

        /// <summary>
        /// Gets the cached resolver delegate, if it exists. Otherwise, compiles a new resolver delegate and returns it.
        /// </summary>
        /// <param name="member">The member to create the resolver for.</param>
        /// <param name="resultType">The parent type of the member.</param>
        /// <param name="cache">If a new resolver is compiled, whether to store the resulting delegate in the cache.</param>
        /// <returns>The compiled <see cref="FieldResolverDelegate" /> delegate.</returns>
        FieldResolverDelegate GetOrCompile(
            MemberInfo member,
            Type? resultType = null,
            bool cache = true);

        /// <inheritdoc cref="IResolverCache.GetOrCompile(MemberInfo, Type?, bool)" />
        /// <param name="selector">Expression for selecting which member to compile the resolver from.</param>
        FieldResolverDelegate GetOrCompile<TObject>(
            Expression<Func<TObject, object?>> selector,
            bool cache = true);

        /// <inheritdoc cref="IResolverCache.GetOrCompile{TObject}(Expression{Func{TObject, object?}}, bool)" />
        FieldResolverDelegate GetOrCompile<TObject, TRuntime>(
            Expression<Func<TObject, TRuntime>> selector,
            bool cache = true);
    }

    /// <inheritdoc />
    internal class ResolverCache : IResolverCache
    {
        private readonly IResolverCompiler _resolverCompiler;

        private readonly Dictionary<int, FieldResolverDelegate> _delegateCache = new();

        public ResolverCache(IResolverCompiler resolverCompiler)
            => this._resolverCompiler = resolverCompiler;

        /// <inheritdoc />
        public FieldResolverDelegate? Get(MemberInfo member)
        {
            int memberKey = member.GetHashCode();

            if(this._delegateCache.TryGetValue(memberKey, out FieldResolverDelegate? resolver))
            {
                return resolver;
            }

            return null;
        }

        /// <inheritdoc />
        public FieldResolverDelegate? Get<TObject>(Expression<Func<TObject, object?>> selector) =>
            selector switch
            {
                { Body: MemberExpression memberExpression }
                    => this.Get(memberExpression.Member),

                { Body: MethodCallExpression methodCallExpression }
                    => this.Get(methodCallExpression.Method),

                _ => throw new NotSupportedException("Only members and method-calls can be resolved.")
            };

        /// <inheritdoc />
        public FieldResolverDelegate? Get<TObject, TRuntime>(Expression<Func<TObject, TRuntime>> selector) =>
            selector switch
            {
                { Body: MemberExpression memberExpression }
                    => this.Get(memberExpression.Member),

                { Body: MethodCallExpression methodCallExpression }
                    => this.Get(methodCallExpression.Method),

                _ => throw new NotSupportedException("Only members and method-calls can be resolved.")
            };

        /// <inheritdoc />
        public FieldResolverDelegate Compile(
            MemberInfo member,
            Type? resultType = null,
            bool cache = true)
        {
            FieldResolverDelegate resolver = this._resolverCompiler.CompileResolver(
                member,
                resultType
            );

            if(cache)
            {
                int memberKey = member.GetHashCode();
                this._delegateCache.Set(memberKey, resolver);
            }

            return resolver;
        }

        /// <inheritdoc />
        public FieldResolverDelegate Compile<TObject>(
            Expression<Func<TObject, object?>> selector,
            bool cache = true) =>
            selector switch
            {
                { Body: MemberExpression memberExpression }
                    => this.Compile(memberExpression.Member, typeof(TObject), cache),

                { Body: MethodCallExpression methodCallExpression }
                    => this.Compile(methodCallExpression.Method, typeof(TObject), cache),

                _ => throw new NotSupportedException("Only members and method-calls can be resolved.")
            };

        /// <inheritdoc />
        public FieldResolverDelegate Compile<TObject, TRuntime>(
            Expression<Func<TObject, TRuntime>> selector,
            bool cache = true) =>
            selector switch
            {
                { Body: MemberExpression memberExpression }
                    => this.Compile(memberExpression.Member, typeof(TObject), cache),

                { Body: MethodCallExpression methodCallExpression }
                    => this.Compile(methodCallExpression.Method, typeof(TObject), cache),

                _ => throw new NotSupportedException("Only members and method-calls can be resolved.")
            };

        /// <inheritdoc />
        public FieldResolverDelegate GetOrCompile(
            MemberInfo member,
            Type? resultType = null,
            bool cache = true)
            => this.Get(member) ?? this.Compile(member, resultType, cache);

        /// <inheritdoc />
        public FieldResolverDelegate GetOrCompile<TObject>(
            Expression<Func<TObject, object?>> selector,
            bool cache = true)
            => this.Get<TObject>(selector) ?? this.Compile<TObject>(selector, cache);

        /// <inheritdoc />
        public FieldResolverDelegate GetOrCompile<TObject, TRuntime>(
            Expression<Func<TObject, TRuntime>> selector,
            bool cache = true)
            => this.Get<TObject, TRuntime>(selector) ?? this.Compile<TObject, TRuntime>(selector, cache);
    }
}