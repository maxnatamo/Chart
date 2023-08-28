using Chart.Language.SyntaxTree;

using static Chart.Language.SyntaxTree.GraphDirectiveLocation;

namespace Chart.Core
{
    public class SpecifiedByDirective : DirectiveDefinition
    {
        public static string Alias => "specifiedBy";
        public static GraphDirectiveLocation Location => SCALAR;

        public SpecifiedByDirective()
            : base(Alias, Location)
        {
            this.Description = "Used to specify any related related human-readable specification for custom scalars.";

            this.Arguments.Add(new ArgumentDefinition<NonNullType<StringType>>("url"));
        }
    }
}