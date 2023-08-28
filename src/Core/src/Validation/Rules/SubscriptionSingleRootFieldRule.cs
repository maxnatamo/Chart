using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Subscription-Operation-Definitions" />
    /// </summary>
    public class SubscriptionSingleRootFieldRule : DocumentValidationRule
    {
        public override IValidationVisitor CreateVisitor()
            => new NodeValidationVisitor<GraphSubscriptionOperation, DocumentValidationContext>(this.ValidateAsync);

        private async Task ValidateAsync(GraphSubscriptionOperation operation, DocumentValidationContext context)
        {
            List<GraphSelection> selections = operation.Selections.Selections;

            if(selections.Count != 1)
            {
                context.RaiseError(DefaultErrors.SubscriptionRootFieldMissing(operation));
                return;
            }

            if(this.HasIntrospectionName(selections[0]))
            {
                context.RaiseError(DefaultErrors.SubscriptionContainsIntrospectionField(operation));
                return;
            }

            GraphSelection? fragment = selections.Find(node =>
                node is GraphFragmentSelection ||
                node is GraphInlineFragmentSelection);

            if(fragment is null)
            {
                return;
            }

            if(fragment is GraphFragmentSelection fragmentSelection)
            {
                if(!context.Query.TryGetDefinition(
                    fragmentSelection.Name,
                    out GraphFragmentDefinition? fragmentDefinition))
                {
                    return;
                }

                selections = fragmentDefinition.SelectionSet.Selections;
            }
            else if(fragment is GraphInlineFragmentSelection inlineSelection)
            {
                selections = inlineSelection.SelectionSet.Selections;
            }

            if(selections.Count != 1)
            {
                context.RaiseError(DefaultErrors.SubscriptionRootFieldMissing(operation));
                return;
            }

            if(this.HasIntrospectionName(selections[0]))
            {
                context.RaiseError(DefaultErrors.SubscriptionContainsIntrospectionField(operation));
                return;
            }

            await Task.CompletedTask;
        }

        private bool HasIntrospectionName(GraphSelection selection)
        {
            string? name = selection switch
            {
                GraphFieldSelection _selection => _selection.Name?.Value,
                GraphFragmentSelection _selection => _selection.Name?.Value,

                _ => null
            };

            return name is not null && this.HasIntrospectionName(name);
        }

        private bool HasIntrospectionName(string name)
            => name.StartsWith("__");
    }
}