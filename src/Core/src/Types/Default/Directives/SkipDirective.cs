using Chart.Language.SyntaxTree;

using static Chart.Language.SyntaxTree.GraphDirectiveLocation;

namespace Chart.Core
{
    public class SkipDirective : DirectiveDefinition
    {
        public static string Alias => "skip";
        public static GraphDirectiveLocation Location => FIELD | FRAGMENT_SPREAD | INLINE_FRAGMENT;

        public SkipDirective()
            : base(Alias, Location)
        {
            this.Description = "Skip this field if the argument is true.";

            this.Arguments.Add(new ArgumentDefinition<NonNullType<BooleanType>>("if"));
        }
    }
}