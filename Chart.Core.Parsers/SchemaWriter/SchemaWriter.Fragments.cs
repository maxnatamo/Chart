using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphFragmentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphFragmentDefinition definition)
        {
            this.WriteLine("fragment ");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

            this.Write($" on {definition.Type.ToString()}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.SelectionSet);
        }

        /// <summary>
        /// Descend into a GraphFragmentSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphFragmentSelection definition)
        {
            this.Write("...");
            this.Write(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.WriteLine("");
        }

        /// <summary>
        /// Descend into a GraphInlineFragmentSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphInlineFragmentSelection definition)
        {
            this.Write("...");

            if(definition.TypeCondition != null)
            {
                this.Write(" on ");
                this.Write(definition.TypeCondition.ToString());
                this.Write(" ");
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.SelectionSet);

            this.WriteLine("");
        }
    }
}