namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphInterfaceDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInterfaceDefinition definition)
        {
            this.context.WriteLine("interface ");

            if(definition.Name != null)
            {
                this.context.Write(definition.Name.ToString());
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
            this.context.Write(" implements ");

            for(int i = 0; i < definition.Implements.Count; i++)
            {
                this.context.Write(definition.Implements[i].Name.ToString());

                if(i < definition.Implements.Count - 1)
                {
                    this.context.Write(" & ");
                }
            }
        }
    }
}