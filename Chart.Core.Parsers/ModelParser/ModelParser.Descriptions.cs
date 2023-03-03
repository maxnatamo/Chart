using System.Reflection;
using Chart.Models.AST;
using Chart.Shared.Attributes;
using Chart.Shared.Extensions;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
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