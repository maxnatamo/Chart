namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphFragmentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFragmentDefinition definition)
        {
            this.context.WriteLine($"[GraphType] {definition.Type.ToString()}");

            this.Visit(definition.SelectionSet);
        }

        /// <summary>
        /// Descend into a GraphFragmentSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFragmentSelection definition)
        {
            this.context.WriteLine($"[GraphName] {definition.Name.ToString()}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }
        }

        /// <summary>
        /// Descend into a GraphInlineFragmentSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInlineFragmentSelection definition)
        {
            if(definition.TypeCondition != null)
            {
                this.context.WriteLine($"[GraphType] {definition.TypeCondition.ToString()}");
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.SelectionSet);
        }
    }
}