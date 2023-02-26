namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphVariables-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphVariables definition)
        {
            if(!definition.Variables.Any())
            {
                return;
            }

            this.context.WriteLine("(");
            this.context.Descend();

            definition.Variables.ForEach(this.Visit);

            this.context.Ascend();
            this.context.WriteLine(")");
        }

        /// <summary>
        /// Descend into a GraphVariable-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphVariable definition)
        {
            this.context.WriteLine($"${definition.Name.Value} : {definition.Type.ToString()}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }
        }
    }
}