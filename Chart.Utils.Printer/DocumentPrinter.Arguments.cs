using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphArguments-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphArguments definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Arguments.ForEach(this.Visit);
            this.DecreaseDepth();
        }

        /// <summary>
        /// Descend into a GraphArgument-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphArgument definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            this.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.Visit(definition.Value);

            this.DecreaseDepth();
        }
    }
}