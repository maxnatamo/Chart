using Chart.Language.SyntaxTree;

using static Chart.Language.SyntaxTree.GraphDirectiveLocationFlags;

namespace Chart.Core
{
    public class DeprecatedDirective : DirectiveDefinition
    {
        public static string Alias => "deprecated";
        public static GraphDirectiveLocationFlags Location => FIELD | FRAGMENT_SPREAD | INLINE_FRAGMENT;

        public DeprecatedDirective()
            : base(Alias, Location)
        {
            this.Description = "Marks the schema definition as deprecated.";

            this.Arguments.Add(new ArgumentDefinition<StringType>(
                name: "reason",
                defaultValue: new GraphNullValue()
            ));
        }
    }
}