using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphTypeDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphTypeDefinition definition)
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
                    throw new NotSupportedException(definition.TypeKind.ToString());
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInputDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphInputDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphObjectType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphObjectType definition)
        {
            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphScalarType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphScalarType definition)
        {
            // Intentionally left empty

            return this;
        }
    }
}