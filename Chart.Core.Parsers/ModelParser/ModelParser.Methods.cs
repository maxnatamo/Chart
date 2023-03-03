using System.Reflection;
using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class ModelParser
    {
        /// <summary>
        /// Parse a MethodInfo-struct into a GraphField-object.
        /// </summary>
        /// <param name="info">The MethodInfo structure to parse.</param>
        /// <returns>The parsed GraphField-object.</returns>
        /// <exception cref="NotImplementedException">Thrown if the MemberType is not valid to parse.</exception>
        /// <exception cref="InvalidDataException">Thrown if a member name is invalid.</exception>
        public GraphField ParseMethodField(MethodInfo info)
        {
            GraphField def = new GraphField();

            def.Name = this.ParseName(info);
            def.Description = this.ParseDescription(info);
            def.Directives = this.ParseDirectives(info);
            def.Type = this.Resolver.ResolveType(info.ReturnType);
            def.Arguments = this.ParseMethodArguments(info);

            return def;
        }

        /// <summary>
        /// Parse the arguments from a MethodInfo-struct into a GraphArgumentsDefinition-object.
        /// </summary>
        /// <param name="info">The MethodInfo structure to parse.</param>
        /// <returns>The parsed GraphArgumentsDefinition-object.</returns>
        /// <exception cref="NotImplementedException">Thrown if the MemberType is not valid to parse.</exception>
        /// <exception cref="InvalidDataException">Thrown if a member name is invalid.</exception>
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

        /// <summary>
        /// Parse the argument from a ParameterInfo-struct into a GraphArgumentDefinition-object.
        /// </summary>
        /// <param name="info">The ParameterInfo structure to parse.</param>
        /// <returns>The parsed GraphArgumentDefinition-object.</returns>
        /// <exception cref="NotImplementedException">Thrown if the MemberType is not valid to parse.</exception>
        /// <exception cref="InvalidDataException">Thrown if a member name is invalid.</exception>
        public GraphArgumentDefinition ParseMethodArgument(ParameterInfo info)
        {
            GraphArgumentDefinition def = new GraphArgumentDefinition();

            def.Name = this.ParseName(info);
            def.Description = this.ParseDescription(info);
            def.Directives = this.ParseDirectives(info);
            def.Type = this.Resolver.ResolveType(info.ParameterType);

            if(info.HasDefaultValue && info.DefaultValue != null)
            {
                def.DefaultValue = this.Resolver.ResolveValue(info.DefaultValue);
            }

            return def;
        }
    }
}