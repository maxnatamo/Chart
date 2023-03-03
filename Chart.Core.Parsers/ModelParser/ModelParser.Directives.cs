using System.Reflection;
using Chart.Models.AST;
using Chart.Shared.Attributes;
using Chart.Shared.Extensions;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
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