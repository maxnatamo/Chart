using Chart.Language.SyntaxTree;

using static Chart.Language.SyntaxTree.GraphDirectiveLocationFlags;

namespace Chart.Core
{
    public class IncludeDirective : DirectiveDefinition
    {
        public static string Alias => "include";
        public static GraphDirectiveLocationFlags Location => FIELD | FRAGMENT_SPREAD | INLINE_FRAGMENT;

        public IncludeDirective()
            : base(Alias, Location)
        {
            this.Description = "Include this field if the argument is true.";

            this.Arguments.Add(new ArgumentDefinition<NonNullType<BooleanType>>("if"));
        }
    }
}