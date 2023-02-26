namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
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

            this.context.WriteLine("{");
            this.context.Descend();

            definition.Fields.ForEach(this.Visit);

            this.context.Ascend();
            this.context.WriteLine("}");
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
            
            this.context.WriteLine(definition.Name.ToString());

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            this.context.Write(": ");
            this.context.Write(definition.Type.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.context.WriteLine("");
        }

        /// <summary>
        /// Descend into a GraphFieldSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFieldSelection definition)
        {
            if(definition.Alias != null)
            {
                this.context.WriteLine($"{definition.Alias.ToString()}: ");
                this.context.Write(definition.Name.ToString());
            }
            else
            {
                this.context.WriteLine(definition.Name.ToString());
            }

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
                this.Visit(definition.SelectionSet);
            }
        }
    }
}