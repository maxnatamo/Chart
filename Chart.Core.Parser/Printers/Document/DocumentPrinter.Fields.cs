namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphFieldSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFieldSelection definition)
        {
            this.context.WriteLine($"[GraphName] {definition.Name.ToString()}");

            if(definition.Alias != null)
            {
                this.context.WriteLine($"[GraphAlias] {definition.Alias.ToString()}");
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

        /// <summary>
        /// Descend into a GraphFields-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFields definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();

            definition.Fields.ForEach(this.Visit);

            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphField-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphField definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();

            this.context.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.context.WriteLine($"[GraphType] {definition.Type.ToString()}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            this.context.Ascend();
        }
    }
}