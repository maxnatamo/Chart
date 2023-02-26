namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphArguments-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphArguments definition)
        {
            if(!definition.Arguments.Any())
            {
                return;
            }

            this.context.Write("(");
            definition.Arguments.ForEach(this.Visit);
            this.context.Write(")");
        }

        /// <summary>
        /// Descend into a GraphArgument-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphArgument definition)
        {
            this.context.Write($"{definition.Name.ToString()}: ");
            this.Visit(definition.Value);
        }
    }
}