using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Fragment-Name-Uniqueness" />
    /// </summary>
    public class FragmentNameUniquenessRule : DocumentValidationRule
    {
        public override IValidationVisitor CreateVisitor()
            => new NodeValidationVisitor<GraphFragmentDefinition, DocumentValidationContext>(this.ValidateAsync);

        private async Task ValidateAsync(GraphFragmentDefinition fragment, DocumentValidationContext context)
        {
            if(fragment.Name is null)
            {
                return;
            }

            IEnumerable<GraphFragmentDefinition> fragmentDefinitions = context.Query
                .GetDefinitions<GraphFragmentDefinition>(fragment.Name);

            if(fragmentDefinitions.Count() != 1)
            {
                context.RaiseError(DefaultErrors.AmbiguousFragment(fragment, fragmentDefinitions.Select(v => v.Location)));
                return;
            }

            await Task.CompletedTask;
        }
    }
}