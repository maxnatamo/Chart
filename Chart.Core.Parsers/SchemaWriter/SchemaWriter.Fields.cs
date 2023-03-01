using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphFields-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphFields definition)
        {
            if(!definition.Fields.Any())
            {
                return;
            }

            this.WriteLine("{");
            this.IncreaseDepth();

            definition.Fields.ForEach(this.Visit);

            this.DecreaseDepth();
            this.WriteLine("}");
        }

        /// <summary>
        /// Descend into a GraphField-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphField definition)
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
        }

        /// <summary>
        /// Descend into a GraphFieldSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFieldSelection definition)
        {
            if(definition.Alias != null)
            {
                this.Write($"{definition.Alias.ToString()}: ");
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
                this.Write(" ");
                this.Visit(definition.SelectionSet);
            }
            else
            {
                this.WriteLine("");
            }
        }
    }
}