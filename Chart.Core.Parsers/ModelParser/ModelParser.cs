using System.Reflection;
using Chart.Models.AST;
using Chart.Core.TypeResolver;
using Chart.Shared.Attributes;
using Chart.Shared.Extensions;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
        private readonly Resolver TypeResolver;

        public ModelParser()
        {
            this.TypeResolver = new Resolver();
        }

        public GraphObjectType ConvertType<T>()
            => this.ConvertType(typeof(T));

        public GraphObjectType ConvertType(Type type)
        {
            GraphObjectType def = this.ConvertAnonymousType(type);
            def.Name = this.ParseName(type);

            return def;
        }

        public GraphObjectType ConvertAnonymousType<T>(string name = "object")
            => this.ConvertObjectType(typeof(T));

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