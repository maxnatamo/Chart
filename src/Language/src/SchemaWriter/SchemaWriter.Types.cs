using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphTypeDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphTypeDefinition definition)
        {
            switch(definition.TypeKind)
            {
                case GraphTypeDefinitionKind.Scalar:
                    this.Visit((GraphScalarType) definition);
                    break;

                case GraphTypeDefinitionKind.Object:
                    this.Visit((GraphObjectType) definition);
                    break;

                default:
                    throw new NotSupportedException(definition.TypeKind.ToString());
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphScalarType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphScalarType definition)
        {
            this.Write(Keywords.Scalar);
            this.WriteSpace();

            this.Write(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.WriteLine();

            return this;
        }

        /// <summary>
        /// Descend into a GraphObjectType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphObjectType definition)
        {
            this.Write(Keywords.Type);
            this.WriteSpace();

            this.Write(definition.Name.ToString());

            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Fields != null && definition.Fields.Fields.Any())
            {
                this.WriteSpace();
                this.Visit(definition.Fields);
            }
            else
            {
                this.WriteLine();
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInputDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphInputDefinition definition)
        {
            this.Write(Keywords.Input);
            this.WriteSpace();

            this.Write(definition.Name.ToString());

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
    }
}