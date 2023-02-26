using System.Reflection;

namespace Chart.Core.Parser
{
    public partial class Serializer
    {
        private readonly TypeResolver TypeResolver;

        public Serializer()
        {
            this.TypeResolver = new TypeResolver();
        }

        public GraphDefinition ConvertType<T>()
        {
            return this.ConvertObjectType<T>();
        }

        public GraphScalarType ConvertScalarType<T>()
        {
            return new GraphScalarType();
        }

        public GraphObjectType ConvertObjectType<T>()
        {
            return this.ConvertObjectType(typeof(T));
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