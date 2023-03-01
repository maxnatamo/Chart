using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphArgumentsDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphArgumentsDefinition definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Arguments.ForEach(this.Visit);
            this.DecreaseDepth();
        }

        /// <summary>
        /// Descend into a GraphArgumentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphArgumentDefinition definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            this.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.WriteLine($"[GraphType] {definition.Type.ToString()}");

            if(definition.DefaultValue != null)
            {
                this.Visit(definition.DefaultValue);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.DecreaseDepth();
        }
    }
}