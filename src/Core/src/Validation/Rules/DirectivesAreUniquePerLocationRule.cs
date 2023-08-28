using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Directives-Are-Unique-Per-Location" />
    /// </summary>
    public class DirectivesAreUniquePerLocationRule : DocumentValidationRule
    {
        private readonly Dictionary<string, GraphDirectiveDefinition> _directiveDefinitions = new();

        public override IValidationVisitor CreateVisitor() => new ValidationVisitors(
            new NodeValidationVisitor<GraphDirectives, ValidationContext>(this.ValidateAsync));

        private async Task ValidateAsync(GraphDirectives directives, ValidationContext context)
        {
            foreach(GraphDirective directive in directives.Directives)
            {
                // In a vacuum, we would need to raise an error when the definition isn't found.
                // But, we're counting on other validation rules to catch non-existing definitions.
                GraphDirectiveDefinition? directiveDefinition = this.GetOrRetrieveDirectiveDefinition(directive.Name, context);

                if(directiveDefinition is null || directiveDefinition.Repeatable)
                {
                    continue;
                }

                if(directives.Directives.Count(v => v.Name == directive.Name) != 1)
                {
                    context.RaiseError(DefaultErrors.DirectiveNonUnique(directive));
                    return;
                }
            }

            await Task.CompletedTask;
        }

        private GraphDirectiveDefinition? GetOrRetrieveDirectiveDefinition(string name, ValidationContext context)
        {
            if(this._directiveDefinitions.TryGetValue(name, out GraphDirectiveDefinition? directiveDefinition))
            {
                return directiveDefinition;
            }

            if(context.Schema.TryGetDefinition(name, out GraphDirectiveDefinition? directiveDefinition1))
            {
                return directiveDefinition1;
            }

            return null;
        }
    }
}