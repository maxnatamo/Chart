namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphVariables-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphVariables definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            definition.Variables.ForEach(this.Visit);
            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphVariable-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphVariable definition)
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