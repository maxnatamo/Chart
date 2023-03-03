using System.Reflection;
using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
        /// <summary>
        /// Parse a FieldInfo-struct into a GraphField-object.
        /// </summary>
        /// <param name="info">The FieldInfo structure to parse.</param>
        /// <returns>The parsed GraphField-object.</returns>
        /// <exception cref="NotImplementedException">Thrown if the MemberType is not valid to parse.</exception>
        /// <exception cref="InvalidDataException">Thrown if a member name is invalid.</exception>
        public GraphField ParseFieldsField(FieldInfo info)
        {
            GraphField def = new GraphField();

            def.Name = this.ParseName(info);
            def.Description = this.ParseDescription(info);
            def.Directives = this.ParseDirectives(info);
            def.Type = this.Resolver.ResolveType(info.FieldType);

            return def;
        }
    }
}