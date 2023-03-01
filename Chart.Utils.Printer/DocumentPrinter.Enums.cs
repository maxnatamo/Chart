using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphEnumDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphEnumDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Values);
        }

        /// <summary>
        /// Descend into a GraphEnumValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphEnumDefinitionValues definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Values.ForEach(this.Visit);
            this.DecreaseDepth();
        }

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphEnumDefinitionValue definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.WriteLine($"[GraphName] {definition.Name.ToString()}");

            this.DecreaseDepth();
        }
    }
}