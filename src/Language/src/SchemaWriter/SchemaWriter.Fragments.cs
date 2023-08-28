using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphFragmentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphFragmentDefinition definition)
        {
            this.Write(Keywords.Fragment);
            this.WriteSpace();

            this.Write(definition.Name.ToString());

            this.WriteSpace();
            this.Write(Keywords.On);
            this.WriteSpace();
            this.Write(definition.Type.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.SelectionSet);

            return this;
        }

        /// <summary>
        /// Descend into a GraphFragmentSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphFragmentSelection definition)
        {
            this.Write("...");
            this.Write(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.WriteLine();

            return this;
        }

        /// <summary>
        /// Descend into a GraphInlineFragmentSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphInlineFragmentSelection definition)
        {
            this.Write("...");

            if(definition.TypeCondition != null)
            {
                this.WriteSpace();
                this.Write(Keywords.On);
                this.WriteSpace();

                this.Write(definition.TypeCondition.ToString());
                this.WriteSpace();
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.SelectionSet);

            this.WriteLine();

            return this;
        }
    }
}