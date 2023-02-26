namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphFragmentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphFragmentDefinition definition)
        {
            this.context.WriteLine("fragment ");

            if(definition.Name != null)
            {
                this.context.Write(definition.Name.ToString());
            }

            this.context.Write($" on {definition.Type.ToString()}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.SelectionSet);
        }
    }
}