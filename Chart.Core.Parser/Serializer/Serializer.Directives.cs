using System.Reflection;

namespace Chart.Core.Parser
{
    public partial class Serializer
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