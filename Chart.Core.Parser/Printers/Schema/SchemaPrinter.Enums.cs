namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphEnumDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphEnumDefinition definition)
        {
            this.context.WriteLine("enum ");

            if(definition.Name != null)
            {
                this.context.Write(definition.Name.ToString());
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Values);
        }

        /// <summary>
        /// Descend into a GraphEnumDefinitionValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphEnumDefinitionValues definition)
        {
            this.context.WriteLine("{");
            this.context.Descend();

            definition.Values.ForEach(this.Visit);

            this.context.Ascend();
            this.context.WriteLine("}");
        }

        /// <summary>
        /// Descend into a GraphEnumDefinitionValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphEnumDefinitionValue definition)
        {
            if(definition.Description != null)
            {
                this.Visit(definition.Description);
            }

            this.context.WriteLine(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }
        }
    }
}