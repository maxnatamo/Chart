using Chart.Language.SyntaxTree;

namespace Chart.Core.Validation
{
    /// <summary>
    /// Validation rule for <see href="https://spec.graphql.org/October2021/#sec-Directives-Are-In-Valid-Locations" />
    /// </summary>
    public class DirectivesAreInValidLocationRule : DocumentValidationRule
    {
        public override IValidationVisitor CreateVisitor() => new ValidationVisitors(
            new NodeValidationVisitor<GraphEnumDefinition, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphExtensionDefinition, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphInputDefinition, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphInterfaceDefinition, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphOperationDefinition, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphSchemaDefinition, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphTypeDefinition, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphUnionDefinition, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphArgumentDefinition, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphField, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphSelection, ValidationContext>(this.ValidateAsync),
            new NodeValidationVisitor<GraphVariable, ValidationContext>(this.ValidateAsync));

        private async Task ValidateAsync(IHasDirectives node, ValidationContext context)
            => await this.ValidateDirectivesAsync(node.Directives, DirectiveLocations.GetLocation(node), context);

        private async Task ValidateDirectivesAsync(
            GraphDirectives? directives,
            GraphDirectiveLocationFlags nodeLocation,
            ValidationContext context)
        {
            if(directives is null)
            {
                return;
            }

            foreach(GraphDirective directive in directives.Directives)
            {
                await this.ValidateDirectiveAsync(
                    directive,
                    nodeLocation,
                    context);
            }
        }

        private async Task ValidateDirectiveAsync(
            GraphDirective directive,
            GraphDirectiveLocationFlags nodeLocation,
            ValidationContext context)
        {
            if(!context.Schema.TryGetDefinition(directive.Name, out GraphDirectiveDefinition? directiveDefinition))
            {
                return;
            }

            if((directiveDefinition.Locations.Locations & nodeLocation) == 0)
            {
                context.RaiseError(DefaultErrors.DirectiveInvalidLocation(
                    directive,
                    directiveDefinition,
                    nodeLocation));
                return;
            }

            await Task.CompletedTask;
        }
    }
}