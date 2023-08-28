using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphUnionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphUnionDefinition definition)
        {
            this.Write(Keywords.Union);
            this.WriteSpace();

            this.Write(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Members != null)
            {
                this.Visit(definition.Members);
            }

            this.WriteLine();

            return this;
        }

        /// <summary>
        /// Descend into a GraphUnionMembers-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphUnionMembers definition)
        {
            this.Write(" = ");
            this.WriteMany(definition.Members, v => this.Write(v.ToString()), " | ");

            return this;
        }
    }
}