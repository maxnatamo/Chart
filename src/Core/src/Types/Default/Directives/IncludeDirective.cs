using Chart.Language.SyntaxTree;

using static Chart.Language.SyntaxTree.GraphDirectiveLocation;

namespace Chart.Core
{
    public class IncludeDirective : DirectiveDefinition
    {
        public static string Alias => "include";
        public static GraphDirectiveLocation Location => FIELD | FRAGMENT_SPREAD | INLINE_FRAGMENT;

        public IncludeDirective()
            : base(Alias, Location)
        {
            this.Description = "Include this field if the argument is true.";

            this.Arguments.Add(new ArgumentDefinition<NonNullType<BooleanType>>("if"));
        }
    }
}