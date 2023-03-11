using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphUnionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphUnionDefinition definition)
        {
            this.Write("union ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Members != null)
            {
                this.Visit(definition.Members);
            }

            this.WriteLine("");
        }

        /// <summary>
        /// Descend into a GraphUnionMembers-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphUnionMembers definition)
        {
            this.Write(" = ");

            for(int i = 0; i < definition.Members.Count; i++)
            {
                this.Write(definition.Members[i].ToString());

                if(i < definition.Members.Count - 1)
                {
                    this.Write(" | ");
                }
            }
        }
    }
}