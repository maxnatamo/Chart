using System.Reflection;
using Chart.Models.AST;
using Chart.Shared.Attributes;
using Chart.Shared.Extensions;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
        /// <summary>
        /// Parse GraphName-attributes from a member and return it.
        /// </summary>
        /// <param name="info">The member to parse the name from.</param>
        /// <returns>GraphName-attribute value, if one is found. Otherwise, member name is returned.</returns>
        /// <exception cref="InvalidDataException">Thrown if a member name is invalid.</exception>
        public GraphName ParseName(MemberInfo info)
        {
            GraphName? name = info.GetAttribute<GraphNameAttribute>()?.Name;

            if(name == null)
            {
                name = new GraphName(info.Name);
                name.Value = char.ToLower(name.Value[0]) + name.Value.Substring(1);
            }

            return name;
        }

        /// <summary>
        /// Parse GraphName-attributes from a member and return it.
        /// </summary>
        /// <param name="info">The member to parse the name from.</param>
        /// <returns>GraphName-attribute value, if one is found. Otherwise, member name is returned.</returns>
        /// <exception cref="InvalidDataException">Thrown if a member name is invalid.</exception>
        public GraphName ParseName(ParameterInfo info)
        {
            GraphName? name = info.GetAttribute<GraphNameAttribute>()?.Name;

            if(name == null)
            {
                name = new GraphName(info.Name ?? "");
                name.Value = char.ToLower(name.Value[0]) + name.Value.Substring(1);
            }

            return name;
        }
    }
}