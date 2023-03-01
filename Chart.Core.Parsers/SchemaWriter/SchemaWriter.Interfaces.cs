using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphInterfaceDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInterfaceDefinition definition)
        {
            this.WriteLine("interface ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

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
        }

        /// <summary>
        /// Descend into a GraphInterfaces-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInterfaces definition)
        {
            this.Write(" implements ");

            for(int i = 0; i < definition.Implements.Count; i++)
            {
                this.Write(definition.Implements[i].Name.ToString());

                if(i < definition.Implements.Count - 1)
                {
                    this.Write(" & ");
                }
            }
        }
    }
}