using System.Reflection;

namespace Chart.Core.Parser
{
    public partial class Serializer
    {
        public GraphField ParsePropertyField(PropertyInfo info)
        {
            GraphField def = new GraphField();

            def.Name = this.ParseName(info);
            def.Description = this.ParseDescription(info);
            def.Directives = this.ParseDirectives(info);
            def.Type = this.TypeResolver.ResolveType(info.PropertyType);

            return def;
        }
    }
}