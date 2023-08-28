using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Fragment-Spread-Type-Existence" />
    /// </summary>
    public class FragmentSpreadTypeExistenceRule : DocumentValidationRule
    {
        public override IValidationVisitor CreateVisitor() => new ValidationVisitors(
            new NodeValidationVisitor<GraphInlineFragmentSelection, DocumentValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphFragmentDefinition, DocumentValidationContext>(this.ValidateAsync));

        private async Task ValidateAsync(GraphInlineFragmentSelection selection, DocumentValidationContext context)
        {
            GraphNamedType? namedSpread = selection.TypeCondition;
            if(namedSpread is null)
            {
                return;
            }

            if(!context.Schema.TryGetDefinition(namedSpread.Name, out GraphTypeDefinition? _))
            {
                context.RaiseError(DefaultErrors.FragmentTypeNotFound(selection));
                return;
            }

            await Task.CompletedTask;
        }

        private async Task ValidateAsync(GraphFragmentDefinition definition, DocumentValidationContext context)
        {
            GraphNamedType? namedSpread = definition.Type;

            if(!context.Schema.TryGetDefinition(namedSpread.Name, out GraphTypeDefinition? _))
            {
                context.RaiseError(DefaultErrors.FragmentTypeNotFound(definition));
                return;
            }

            await Task.CompletedTask;
        }
    }
}