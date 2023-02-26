namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphSchemaDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphSchemaDefinition definition)
        {
            this.context.WriteLine("schema ");

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
            this.context.WriteLine("{");
            this.context.Descend();

            definition.Values.ForEach(this.Visit);

            this.context.Ascend();
            this.context.WriteLine("}");
        }

        /// <summary>
        /// Descend into a GraphSchemaValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSchemaValue definition)
        {
            this.context.WriteLine(definition.Operation.ToString());
            this.context.Write(": ");
            this.context.Write(definition.Type.ToString());
        }
    }
}