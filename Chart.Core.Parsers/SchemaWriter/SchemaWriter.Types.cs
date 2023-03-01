using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphTypeDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphTypeDefinition definition)
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
                    throw new NotImplementedException(definition.TypeKind.ToString());
            }
        }

        /// <summary>
        /// Descend into a GraphScalarType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphScalarType definition)
        {
            this.WriteLine($"scalar {definition.Name}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }
        }

        /// <summary>
        /// Descend into a GraphObjectType-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphObjectType definition)
        {
            this.Write($"type {definition.Name} ");

            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }
            else
            {
                this.WriteLine("");
            }
        }

        /// <summary>
        /// Descend into a GraphInputDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInputDefinition definition)
        {
            this.WriteLine("input ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Arguments);
        }
    }
}