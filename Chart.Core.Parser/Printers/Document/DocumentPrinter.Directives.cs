namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphDirectiveDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDirectiveDefinition definition)
        {
            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            if(definition.Repeatable)
            {
                this.context.WriteLine($"[GraphValue] Repeatable");
            }

            this.Visit(definition.Locations);
        }

        /// <summary>
        /// Descend into a GraphDirectiveLocations-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDirectiveLocations definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            definition.Locations.ForEach(v =>
            {
                this.context.WriteLine($"[GraphName] {v.Value}");
            });
            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphDirectives-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDirectives definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();

            definition.Directives.ForEach(this.Visit);

            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphDirective-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDirective definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            
            this.context.WriteLine($"[GraphName] {definition.Name.ToString()}");

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            this.context.Ascend();
        }
    }
}