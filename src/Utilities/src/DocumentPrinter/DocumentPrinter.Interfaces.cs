using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphInterfaceDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphInterfaceDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Interface != null)
            {
                this.Visit(definition.Interface);
            }

            if(definition.Fields != null)
            {
                this.Visit(definition.Fields);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInterfaces-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphInterfaces definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Implements.ForEach(v =>
            {
                this.WriteLine($"[GraphName] {v.ToString()}");
            });
            this.DecreaseDepth();

            return this;
        }
    }
}