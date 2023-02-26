namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphArgumentsDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphArgumentsDefinition definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            definition.Arguments.ForEach(this.Visit);
            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphArgumentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphArgumentDefinition definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            this.context.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.context.WriteLine($"[GraphType] {definition.Type.ToString()}");

            if(definition.DefaultValue != null)
            {
                this.Visit(definition.DefaultValue);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.context.Ascend();
        }
    }
}