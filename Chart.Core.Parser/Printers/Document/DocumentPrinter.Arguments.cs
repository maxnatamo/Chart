namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphArguments-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphArguments definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();
            definition.Arguments.ForEach(this.Visit);
            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphArgument-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphArgument definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();

            this.context.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.Visit(definition.Value);

            this.context.Ascend();
        }
    }
}