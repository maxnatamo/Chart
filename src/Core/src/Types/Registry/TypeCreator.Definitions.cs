using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public partial class TypeCreator : ITypeCreator
    {
        /// <inheritdoc />
        public ITypeDefinition CreateFromDefinition(GraphDefinition definition) =>
            definition switch
            {
                GraphObjectType _definition => this.CreateFromGraphType(_definition),

                _ => throw new NotSupportedException()
            };

        private ITypeDefinition CreateFromGraphType(GraphObjectType definition)
        {
            ObjectType typeDefinition = new(
                name: definition.Name,
                description: definition.Description?.Value);

            if(definition.Directives is not null)
            {
                foreach(GraphDirective directive in definition.Directives.Directives)
                {
                    typeDefinition.Directives.Add(
                        this.CreateDirectiveFromGraphType(directive));
                }
            }

            if(definition.Fields is not null)
            {
                foreach(GraphField field in definition.Fields.Fields)
                {
                    typeDefinition.Fields.Add(
                        this.CreateFieldFromGraphType(field));
                }
            }

            if(definition.Interface is not null)
            {
                // TODO
            }

            return typeDefinition;
        }

        private Directive CreateDirectiveFromGraphType(GraphDirective directive)
        {
            Directive directiveTypeDefinition = new(directive.Name);

            if(directive.Arguments is not null)
            {
                foreach(GraphArgument argument in directive.Arguments.Arguments)
                {
                    directiveTypeDefinition.Arguments.Add(new(argument.Name, argument.Value));
                }
            }

            return directiveTypeDefinition;
        }

        private FieldDefinition CreateFieldFromGraphType(GraphField field)
        {
            FieldDefinition fieldDefinition = new(
                name: field.Name,
                type: field.Type);

            if(field.Directives is not null)
            {
                foreach(GraphDirective directive in field.Directives.Directives)
                {
                    fieldDefinition.Directives.Add(
                        this.CreateDirectiveFromGraphType(directive));
                }
            }

            if(field.Arguments is not null)
            {
                foreach(GraphArgumentDefinition argument in field.Arguments.Arguments)
                {
                    fieldDefinition.Arguments.Add(
                        this.CreateArgumentDefinitionFromGraphType(argument));
                }
            }

            return fieldDefinition;
        }

        private ArgumentDefinition CreateArgumentDefinitionFromGraphType(GraphArgumentDefinition argument)
        {
            ArgumentDefinition argumentDefinition = new(
                name: argument.Name,
                type: this._typeResolver.CreateTypeDefinition(argument.Type),
                defaultValue: argument.DefaultValue);

            if(argument.Directives is not null)
            {
                foreach(GraphDirective directive in argument.Directives.Directives)
                {
                    argumentDefinition.Directives.Add(
                        this.CreateDirectiveFromGraphType(directive));
                }
            }

            return argumentDefinition;
        }
    }
}