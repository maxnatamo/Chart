using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphSchemaDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphSchemaDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Values);

            return this;
        }

        /// <summary>
        /// Descend into a GraphSchemaValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphSchemaValues definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Values.ForEach(v => this.Visit(v));
            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphSchemaValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphSchemaValue definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            this.WriteLine($"[GraphOperation] {definition.Operation.ToString()}");
            this.WriteLine($"[GraphType] {definition.Type.ToString()}");
            this.DecreaseDepth();

            return this;
        }
    }
}