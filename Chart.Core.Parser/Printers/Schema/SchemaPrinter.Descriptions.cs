namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphDescription-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDescription definition)
        {
            this.context.WriteLine("\"\"\"");
            this.context.WriteLine(definition.Value);
            this.context.WriteLine("\"\"\"");
        }
    }
}