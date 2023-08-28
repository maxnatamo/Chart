using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Lone-Anonymous-Operation" />
    /// </summary>
    public class LoneAnonymousOperationRule : DocumentValidationRule
    {
        public override IValidationVisitor CreateVisitor()
            => new NodeValidationVisitor<GraphOperationDefinition, DocumentValidationContext>(this.ValidateAsync);

        private async Task ValidateAsync(GraphOperationDefinition operation, DocumentValidationContext context)
        {
            if(operation.Name is not null)
            {
                return;
            }

            if(context.Query.GetDefinitions<GraphOperationDefinition>().Count() > 1)
            {
                context.RaiseError(DefaultErrors.InvalidAnonymousOperation(operation));
                return;
            }

            await Task.CompletedTask;
        }
    }
}