using System.Reflection;
using Chart.Models.AST;
using Chart.Shared.Attributes;
using Chart.Shared.Extensions;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
        /// <summary>
        /// Parse GraphDescriptionAttribute-attributes from a member and return it.
        /// </summary>
        /// <param name="info">The member to parse the description from.</param>
        /// <returns>GraphDescription, if one is found. Otherwise, null.</returns>
        public GraphDescription? ParseDescription(ICustomAttributeProvider info)
        {
            var attr = info.GetAttribute<GraphDescriptionAttribute>();
            if(attr != null)
            {
                return new GraphDescription(attr.Description);
            }

            return null;
        }
    }
}