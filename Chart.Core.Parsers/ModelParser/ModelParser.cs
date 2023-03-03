using System.Reflection;
using Chart.Models.AST;
using Chart.Core.TypeResolver;
using Chart.Shared.Attributes;
using Chart.Shared.Extensions;

namespace Chart.Core.Parsers
{
    /// <summary>
    /// Class for parsing C# models into an IR-format.
    /// </summary>
    public partial class ModelParser
    {
        /// <summary>
        /// Internal resolver for values and types.
        /// </summary>
        private readonly Resolver Resolver;

        /// <summary>
        /// Initialize new ModelParser.
        /// </summary>
        public ModelParser()
        {
            this.Resolver = new Resolver();
        }

        /// <summary>
        /// Convert type into the IR-format and return it.
        /// </summary>
        /// <remarks>
        /// The type is not registered to the resolver, so unregistered child-types will throw.
        /// </remarks>
        /// <typeparam name="T">The type to convert into IR-format.</typeparam>
        /// <returns>The IR-format in an AST.</returns>
        public GraphObjectType ConvertType<T>()
            => this.ConvertType(typeof(T));

        /// <summary>
        /// Convert type into the IR-format and return it.
        /// </summary>
        /// <remarks>
        /// The type is not registered to the resolver, so unregistered child-types will throw.
        /// </remarks>
        /// <param name="type">The type to convert into IR-format.</param>
        /// <returns>The IR-format in an AST.</returns>
        /// <exception cref="InvalidDataException">Thrown when the object name is an invalid GraphQL-name.</exception>
        public GraphObjectType ConvertType(Type type)
        {
            GraphObjectType def = this.ConvertAnonymousType(type);
            def.Name = this.ParseName(type);

            return def;
        }

        /// <summary>
        /// Convert anonymous type into the IR-format and return it.
        /// </summary>
        /// <remarks>
        /// The type is not registered to the resolver, so unregistered child-types will throw.
        /// </remarks>
        /// <typeparam name="T">The type to convert into IR-format.</typeparam>
        /// <param name="name">The to give to the object.</param>
        /// <returns>The IR-format in an AST.</returns>
        /// <exception cref="InvalidDataException">Thrown when the object name is an invalid GraphQL-name.</exception>
        public GraphObjectType ConvertAnonymousType<T>(string name = "object")
            => this.ConvertAnonymousType(typeof(T), name);

        /// <summary>
        /// Convert anonymous type into the IR-format and return it.
        /// </summary>
        /// <remarks>
        /// The type is not registered to the resolver, so unregistered child-types will throw.
        /// </remarks>
        /// <param name="type">The type to convert into IR-format.</param>
        /// <param name="name">The to give to the object.</param>
        /// <returns>The IR-format in an AST.</returns>
        /// <exception cref="InvalidDataException">Thrown when the object name is an invalid GraphQL-name.</exception>
        public GraphObjectType ConvertAnonymousType(Type type, string name = "object")
        {
            GraphObjectType def = new GraphObjectType();

            def.Name = new GraphName(name);
            def.Description = this.ParseDescription(type);
            def.Directives = this.ParseDirectives(type);
            def.Fields = this.ParseFields(type);

            return def;
        }

        /// <summary>
        /// Whether the include the specified field in the schema
        /// </summary>
        /// <param name="info">The MemberInfo-object to check for.</param>
        /// <returns>True, if the field can be included. Otherwise, false.</returns>
        private bool ShouldIncludeField(MemberInfo info)
            => !info.GetAttributes<GraphIgnoreAttribute>().Any();
    }
}