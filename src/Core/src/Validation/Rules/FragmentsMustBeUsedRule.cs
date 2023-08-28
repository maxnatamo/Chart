using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Fragments-Must-Be-Used" />
    /// </summary>
    public class FragmentsMustBeUsedRule : DocumentValidationRule
    {
        /// <summary>
        /// List of all fragments that have been referenced in the document.
        /// </summary>
        private readonly ISet<GraphName> _referencedFragments = new HashSet<GraphName>();

        public override IValidationVisitor CreateVisitor() => new ValidationVisitors(
            new NodeValidationVisitor<GraphFragmentSelection, DocumentValidationContext>(enter: this.ValidateAsync),
            new NodeValidationVisitor<GraphFragmentDefinition, DocumentValidationContext>(leave: this.ValidateAsync));

        private async Task ValidateAsync(GraphFragmentSelection selection, DocumentValidationContext context)
        {
            this._referencedFragments.Add(selection.Name);
            await Task.CompletedTask;
        }

        private async Task ValidateAsync(GraphFragmentDefinition definition, DocumentValidationContext context)
        {
            if(!this._referencedFragments.Contains(definition.Name))
            {
                context.RaiseError(DefaultErrors.FragmentNotReferenced(definition));
                return;
            }

            await Task.CompletedTask;
        }
    }
}