using Chart.Language.SyntaxTree;
using System.Reflection;

namespace Chart.Core
{
    /// <summary>
    /// Resolver for getting values from attributes on types.
    /// </summary>
    public interface IAttributeResolver
    {
        /// <summary>
        /// Get the name of the given type.
        /// </summary>
        /// <remarks>
        /// If an explicit name is set (via <see cref="GraphNameAttribute" />), it is returned.
        /// Otherwise, the name is inferred from the type name. This inferred name will be formatted.
        /// </remarks>
        /// <param name="type">The type to get the name from.</param>
        /// <returns>The formatted type name.</returns>
        GraphName GetName(
            ICustomAttributeProvider type,
            string? fallback = null,
            Func<string, string>? formatter = null);

        /// <inheritdoc cref="IAttributeResolver.GetName(ICustomAttributeProvider, string?, Func{string, string}?)" />
        GraphName GetName(Type type);

        /// <inheritdoc cref="IAttributeResolver.GetName(ICustomAttributeProvider, string?, Func{string, string}?)" />
        GraphName GetName(MemberInfo member);

        /// <inheritdoc cref="IAttributeResolver.GetName(ICustomAttributeProvider, string?, Func{string, string}?)" />
        GraphName GetName(ParameterInfo member);

        /// <summary>
        /// Get the explicit name of the given type.
        /// </summary>
        /// <remarks>
        /// If an explicit name is set (via <see cref="GraphNameAttribute" />), it is returned. Otherwise, <see langword="null" />.
        /// </remarks>
        /// <param name="type">The type to get the name from.</param>
        /// <returns>The formatted type name, if any is explicitly set. Otherwise, <see langword="null" />.</returns>
        GraphName? GetExplicitName(ICustomAttributeProvider type);

        /// <summary>
        /// Attempt to get the description from the given type.
        /// </summary>
        /// <remarks>
        /// If an explicit description is set (via <see cref="GraphDescriptionAttribute" />), it is returned.
        /// Otherwise, returns <see langword="null" />.
        /// </remarks>
        /// <param name="type">The type to get the description from.</param>
        GraphDescription? GetDescription(ICustomAttributeProvider type);

        /// <summary>
        /// Attempt to get the directives from the given type.
        /// </summary>
        /// <param name="type">The type to get the directives from.</param>
        List<Directive>? GetDirectives(ICustomAttributeProvider type);
    }

    public class AttributeResolver : IAttributeResolver
    {
        private readonly INameFormatter _nameFormatter;
        private readonly IValueRegistry _valueRegistry;

        public AttributeResolver(INameFormatter nameFormatter, IValueRegistry valueRegistry)
        {
            this._nameFormatter = nameFormatter;
            this._valueRegistry = valueRegistry;
        }

        public virtual GraphName GetName(
            ICustomAttributeProvider type,
            string? fallback = null,
            Func<string, string>? formatter = null)
        {
            formatter ??= this._nameFormatter.FormatTypeName;

            GraphName? explicitName = this.GetExplicitName(type);
            if(explicitName is not null)
            {
                return explicitName;
            }

            string formattedTypeName = formatter(fallback ?? type.GetType().Name);
            return new GraphName(formattedTypeName);
        }

        public virtual GraphName GetName(Type type)
        {
            GraphName? explicitName = this.GetExplicitName(type);
            if(explicitName is not null)
            {
                return explicitName;
            }

            string formattedTypeName = this._nameFormatter.GetGenericsName(type);
            return new GraphName(formattedTypeName);
        }

        public virtual GraphName GetName(MemberInfo member)
            => this.GetName(member, member.Name);

        public virtual GraphName GetName(ParameterInfo member)
            => this.GetName(member, member.Name);

        public virtual GraphName? GetExplicitName(ICustomAttributeProvider type)
            => type.GetAttribute<GraphNameAttribute>()?.Name;

        public virtual GraphDescription? GetDescription(ICustomAttributeProvider type)
            => GraphDescription.From(type.GetAttribute<GraphDescriptionAttribute>()?.Description);

        public virtual List<Directive>? GetDirectives(ICustomAttributeProvider type)
        {
            IEnumerable<GraphDirectiveAttribute> directives = type.GetAttributes<GraphDirectiveAttribute>();
            if(!directives.Any())
            {
                return null;
            }

            List<Directive> definition = new();

            foreach(GraphDirectiveAttribute attribute in directives)
            {
                Directive directive = new(attribute.Name);

                if(attribute.Arguments is not null)
                {
                    foreach(KeyValuePair<string, object?> argument in attribute.Arguments)
                    {
                        DirectiveArgumentDefinition argumentDefinition = new(
                            argument.Key,
                            this._valueRegistry.ResolveValue(argument.Value)
                        );

                        directive.Arguments.Add(argumentDefinition);
                    }
                }

                definition.Add(directive);
            }

            return definition;
        }
    }
}