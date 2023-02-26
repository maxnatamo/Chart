using System.Reflection;

namespace Chart.Core.Parser
{
    public partial class Serializer
    {
        public GraphObjectType ConvertObjectType(Type type)
        {
            GraphObjectType def = new GraphObjectType();

            def.Name = this.ParseName(type);
            def.Description = this.ParseDescription(type);
            def.Directives = this.ParseDirectives(type);
            def.Fields = this.ParseFields(type);

            return def;
        }

        private GraphFields? ParseFields(Type type)
        {
            GraphFields def = new GraphFields();

            List<MemberInfo> typeFiles = new List<MemberInfo>();
            typeFiles.AddRange(this.GetFields(type));
            typeFiles.AddRange(this.GetProperties(type));
            typeFiles.AddRange(this.GetMethods(type));

            def.Fields = typeFiles
                .Where(v => this.ShouldIncludeField(v))
                .Select(v => this.ParseField(v))
                .ToList();

            if(def.Fields.Count == 0)
            {
                return null;
            }

            return def;
        }
        
        /// <summary>
        /// Parse a MemberInfo-struct into a GraphField-object.
        /// </summary>
        /// <param name="info">The MemberInfo structure to parse.</param>
        /// <returns>The parsed GraphField-object.</returns>
        /// <throws cref="NotImplementedException">Thrown if the MemberType is not valid to parse.</exception>
        private GraphField ParseField(MemberInfo info)
        {
            return info.MemberType switch
            {
                // MemberTypes.Field       => this.ParseMemberField((FieldInfo) info),
                MemberTypes.Property    => this.ParsePropertyField((PropertyInfo) info),
                MemberTypes.Method      => this.ParseMethodField((MethodInfo) info),

                _ => throw new NotImplementedException()
            };
        }
    }
}