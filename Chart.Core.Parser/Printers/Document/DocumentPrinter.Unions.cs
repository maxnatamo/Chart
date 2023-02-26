namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphUnionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphUnionDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Members != null)
            {
                this.Visit(definition.Members);
            }
        }

        /// <summary>
        /// Descend into a GraphUnionMembers-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphUnionMembers definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            definition.Members.ForEach(v =>
            {
                this.context.WriteLine($"[GraphName] {v.ToString()}");
            });
            this.context.Ascend();
        }
    }
}