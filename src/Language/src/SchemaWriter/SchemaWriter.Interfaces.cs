using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphInterfaceDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphInterfaceDefinition definition)
        {
            this.Write(Keywords.Interface);
            this.WriteSpace();

            this.Write(definition.Name.ToString());
            this.WriteSpace();

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

            return this;
        }

        /// <summary>
        /// Descend into a GraphInterfaces-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphInterfaces definition)
        {
            this.WriteSpace();
            this.Write(Keywords.Implements);
            this.WriteSpace();

            this.WriteMany(definition.Implements, v => this.Write(v.Name), " & ");

            return this;
        }
    }
}