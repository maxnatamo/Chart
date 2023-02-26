namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphUnionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphUnionDefinition definition)
        {
            this.context.WriteLine("union ");

            if(definition.Name != null)
            {
                this.context.Write(definition.Name.ToString());
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Members != null)
            {
                this.Visit(definition.Members);
            }
        }

        /// <summary>
        /// Descend into a GraphUnionMembers-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphUnionMembers definition)
        {
            this.context.Write(" = ");
            
            for(int i = 0; i < definition.Members.Count; i++)
            {
                this.context.Write(definition.Members[i].ToString());

                if(i < definition.Members.Count - 1)
                {
                    this.context.Write(" | ");
                }
            }
        }
    }
}