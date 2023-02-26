namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphEnumDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphEnumDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Values);
        }

        /// <summary>
        /// Descend into a GraphEnumValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphEnumDefinitionValues definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            definition.Values.ForEach(this.Visit);
            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphEnumDefinitionValue definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.context.WriteLine($"[GraphName] {definition.Name.ToString()}");

            this.context.Ascend();
        }
    }
}