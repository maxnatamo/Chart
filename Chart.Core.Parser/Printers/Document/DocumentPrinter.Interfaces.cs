namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphInterfaceDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInterfaceDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }
        }

        /// <summary>
        /// Descend into a GraphInterfaces-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphInterfaces definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            definition.Implements.ForEach(v =>
            {
                this.context.WriteLine($"[GraphName] {v.ToString()}");
            });
            this.context.Ascend();
        }
    }
}