using System.Reflection;
using Chart.Models.AST;
using Chart.Shared.Extensions;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
        /// <summary>
        /// Parse an object type into a GraphObjectType-object.
        /// </summary>
        /// <param name="type">The type to parse.</param>
        /// <returns>The parsed GraphObjectType-object.</returns>
        /// <exception cref="NotImplementedException">Thrown if the MemberType is not valid to parse.</exception>
        /// <exception cref="InvalidDataException">Thrown if a member name is invalid.</exception>
        public GraphObjectType ConvertObjectType(Type type)
        {
            GraphObjectType def = new GraphObjectType();

            def.Name = this.ParseName(type);
            def.Description = this.ParseDescription(type);
            def.Directives = this.ParseDirectives(type);
            def.Fields = this.ParseFields(type);

            return def;
        }

        /// <summary>
        /// Parse the fields from a Type-struct into a GraphFields-object.
        /// </summary>
        /// <param name="type">The type to parse.</param>
        /// <returns>The parsed GraphFields-object, if any fields are found. Otherwise, null.</returns>
        /// <exception cref="NotImplementedException">Thrown if the MemberType is not valid to parse.</exception>
        /// <exception cref="InvalidDataException">Thrown if a member name is invalid.</exception>
        private GraphFields? ParseFields(Type type)
        {
            GraphFields def = new GraphFields();

            List<MemberInfo> typeFiles = new List<MemberInfo>();
            typeFiles.AddRange(type.GetLocalFields());
            typeFiles.AddRange(type.GetLocalProperties());
            typeFiles.AddRange(type.GetLocalMethods());

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
        /// <exception cref="NotImplementedException">Thrown if the MemberType is not valid to parse.</exception>
        /// <exception cref="InvalidDataException">Thrown if a member name is invalid.</exception>
        private GraphField ParseField(MemberInfo info)
        {
            return info.MemberType switch
            {
                MemberTypes.Field       => this.ParseFieldsField((FieldInfo) info),
                MemberTypes.Property    => this.ParsePropertyField((PropertyInfo) info),
                MemberTypes.Method      => this.ParseMethodField((MethodInfo) info),

                _ => throw new NotImplementedException()
            };
        }
    }
}