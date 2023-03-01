using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphSchemaDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSchemaDefinition definition)
        {
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
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Values.ForEach(this.Visit);
            this.DecreaseDepth();
        }

        /// <summary>
        /// Descend into a GraphSchemaValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSchemaValue definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            this.WriteLine($"[GraphOperation] {definition.Operation.ToString()}");
            this.WriteLine($"[GraphType] {definition.Type.ToString()}");
            this.DecreaseDepth();
        }
    }
}