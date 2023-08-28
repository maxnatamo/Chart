using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Fragment-Spread-Type-Existence" />
    /// </summary>
    public class FragmentSpreadTargetDefinedRule : DocumentValidationRule
    {
        public override IValidationVisitor CreateVisitor() =>
            new NodeValidationVisitor<GraphFragmentSelection, DocumentValidationContext>(this.ValidateAsync);

        private async Task ValidateAsync(GraphFragmentSelection selection, DocumentValidationContext context)
        {
            if(!context.Query.TryGetDefinition(selection.Name, out GraphFragmentDefinition? _))
            {
                context.RaiseError(DefaultErrors.FragmentNotFound(selection));
                return;
            }

            await Task.CompletedTask;
        }
    }
}