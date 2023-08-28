using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Operation-Name-Uniqueness" />
    /// </summary>
    public class OperationNameUniquenessRule : DocumentValidationRule
    {
        public override IValidationVisitor CreateVisitor()
            => new NodeValidationVisitor<GraphOperationDefinition, DocumentValidationContext>(this.ValidateAsync);

        private async Task ValidateAsync(GraphOperationDefinition operation, DocumentValidationContext context)
        {
            if(operation.Name is null)
            {
                return;
            }

            IEnumerable<GraphOperationDefinition> operationDefinitions = context.Query
                .GetDefinitions<GraphOperationDefinition>(operation.Name);

            if(operationDefinitions.Count() != 1)
            {
                context.RaiseError(DefaultErrors.OperationAlreadyExists(operation));
                return;
            }

            await Task.CompletedTask;
        }
    }
}