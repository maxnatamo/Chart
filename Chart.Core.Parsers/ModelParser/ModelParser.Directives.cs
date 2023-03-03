using System.Reflection;
using Chart.Models.AST;
using Chart.Shared.Attributes;
using Chart.Shared.Extensions;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
        /// <summary>
        /// Parse GraphDirectives-attributes from a member and return it.
        /// </summary>
        /// <param name="info">The member to parse the directives from.</param>
        /// <returns>GraphDirectives, if one is found. Otherwise, null.</returns>
        public GraphDirectives? ParseDirectives(ICustomAttributeProvider info)
        {
            GraphDirectives def = new GraphDirectives();

            def.Directives = info
                .GetAttributes<GraphDirectiveAttribute>()
                .Select(v => new GraphDirective(v.Name))
                .ToList();

            return def;
        }
    }
}