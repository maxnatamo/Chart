using System.Reflection;

namespace Chart.Core.Parser
{
    public partial class Serializer
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