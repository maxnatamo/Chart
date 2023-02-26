namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphSchemaDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSchemaDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Values);
        }

        /// <summary>
        /// Descend into a GraphSchemaValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSchemaValues definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            definition.Values.ForEach(this.Visit);
            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphSchemaValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSchemaValue definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            this.context.WriteLine($"[GraphOperation] {definition.Operation.ToString()}");
            this.context.WriteLine($"[GraphType] {definition.Type.ToString()}");
            this.context.Ascend();
        }
    }
}