using System.Reflection;

namespace Chart.Core.Parser
{
    public partial class Serializer
    {
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