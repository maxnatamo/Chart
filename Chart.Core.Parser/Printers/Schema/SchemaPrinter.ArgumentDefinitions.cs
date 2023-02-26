namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphArgumentsDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphArgumentsDefinition definition)
        {
            if(!definition.Arguments.Any())
            {
                return;
            }
            
            this.context.WriteLine("(");
            this.context.Descend();

            definition.Arguments.ForEach(this.Visit);

            this.context.Ascend();
            this.context.WriteLine(")");
        }

        /// <summary>
        /// Descend into a GraphArgumentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphArgumentDefinition definition)
        {
            if(definition.Description != null)
            {
                this.Visit(definition.Description);
            }

            this.context.WriteLine(definition.Name.ToString());
            this.context.Write(": ");
            this.context.Write(definition.Type.ToString());

            if(definition.DefaultValue != null)
            {
                this.context.Write(" = ");
                this.Visit(definition.DefaultValue);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }
        }
    }
}