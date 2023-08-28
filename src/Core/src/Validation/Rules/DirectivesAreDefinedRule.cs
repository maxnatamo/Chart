using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Directives-Are-Defined" />
    /// </summary>
    public class DirectivesAreDefinedRule : DocumentValidationRule
    {
        public override IValidationVisitor CreateVisitor() =>
            new NodeValidationVisitor<GraphDirective, ValidationContext>(this.ValidateAsync);

        private async Task ValidateAsync(GraphDirective directive, ValidationContext context)
        {
            if(!context.Schema.TryGetDefinition(directive.Name, out GraphDirectiveDefinition? _))
            {
                context.RaiseError(DefaultErrors.DirectiveNotFound(directive));
                return;
            }

            await Task.CompletedTask;
        }
    }
}