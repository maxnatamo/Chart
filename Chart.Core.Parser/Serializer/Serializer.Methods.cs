using System.Reflection;

namespace Chart.Core.Parser
{
    public partial class Serializer
    {
        public GraphField ParseMethodField(MethodInfo info)
        {
            GraphField def = new GraphField();

            def.Name = this.ParseName(info);
            def.Description = this.ParseDescription(info);
            def.Directives = this.ParseDirectives(info);
            def.Type = this.TypeResolver.ResolveType(info.ReturnType);
            def.Arguments = this.ParseMethodArguments(info);

            return def;
        }

        public GraphArgumentsDefinition? ParseMethodArguments(MethodInfo info)
        {
            if(!info.GetParameters().Any())
            {
                return null;
            }

            GraphArgumentsDefinition def = new GraphArgumentsDefinition();
            def.Arguments = info.GetParameters().Select(this.ParseMethodArgument).ToList();

            return def;
        }

        public GraphArgumentDefinition ParseMethodArgument(ParameterInfo info)
        {
            GraphArgumentDefinition def = new GraphArgumentDefinition();

            def.Name = this.ParseName(info);
            def.Description = this.ParseDescription(info);
            def.Directives = this.ParseDirectives(info);
            def.Type = this.TypeResolver.ResolveType(info.ParameterType);

            if(info.HasDefaultValue && info.DefaultValue != null)
            {
                def.DefaultValue = this.TypeResolver.ResolveValue(info.DefaultValue);
            }

            return def;
        }
    }
}