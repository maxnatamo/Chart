using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Introspection.Reserved-Names" />
    /// </summary>
    public class ReservedNameRule : ValidationRule<ValidationContext>
    {
        private static readonly HashSet<string> _instrospectionNames = new()
        {
            "__schema",
            "__type",
            "__field",
            "__inputvalue",
            "__enumvalue",
            "__directive"
        };

        public override IValidationVisitor CreateVisitor()
            => new NodeValidationVisitor<GraphName, ValidationContext>(this.ValidateAsync);

        private async Task ValidateAsync(GraphName name, ValidationContext context)
        {
            if(_instrospectionNames.Contains(name.Value))
            {
                return;
            }

            if(name.Value.StartsWith("__"))
            {
                context.RaiseError(DefaultErrors.NameNotValid(name));
                return;
            }

            await Task.CompletedTask;
        }
    }
}