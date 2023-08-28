using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphFields-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphFields definition)
        {
            if(!definition.Fields.Any())
            {
                return this;
            }

            this.WriteLine("{");
            this.IncreaseDepth();

            definition.Fields.ForEach(v => this.Visit(v));

            this.DecreaseDepth();
            this.WriteLine("}");

            return this;
        }

        /// <summary>
        /// Descend into a GraphField-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphField definition)
        {
            if(definition.Description != null)
            {
                this.Visit(definition.Description);
            }

            this.Write(definition.Name.ToString());

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            this.Write(": ");
            this.Write(definition.Type.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.WriteLine("");

            return this;
        }

        /// <summary>
        /// Descend into a GraphFieldSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphFieldSelection definition)
        {
            if(definition.Alias is not null)
            {
                this.Write(definition.Alias.ToString());
                this.Write(": ");
            }

            this.Write(definition.Name.ToString());

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.SelectionSet != null)
            {
                this.WriteSpace();
                this.Visit(definition.SelectionSet);
            }
            else
            {
                this.WriteLine();
            }

            return this;
        }
    }
}