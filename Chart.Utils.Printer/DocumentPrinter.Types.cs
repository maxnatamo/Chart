using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphTypeDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphTypeDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }
            
            switch(definition.TypeKind)
            {
                case GraphTypeDefinitionKind.Object:
                    this.Visit((GraphObjectType) definition);
                    break;

                case GraphTypeDefinitionKind.Scalar:
                    this.Visit((GraphScalarType) definition);
                    break;

                default:
                    throw new NotImplementedException(definition.TypeKind.ToString());
            }
        }

        /// <summary>
        /// Descend into a GraphInputDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInputDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Arguments);
        }

        /// <summary>
        /// Descend into a GraphObjectType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphObjectType definition)
        {
            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }
        }

        /// <summary>
        /// Descend into a GraphScalarType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphScalarType definition)
        {
            
        }
    }
}