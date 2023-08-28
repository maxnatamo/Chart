using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphEnumDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphEnumDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Values != null)
            {
                this.Visit(definition.Values);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphEnumValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphEnumDefinitionValues definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Values.ForEach(v => this.Visit(v));
            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphEnumValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphEnumDefinitionValue definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            this.WriteLine($"[GraphName] {definition.Name.ToString()}");

            if(definition.Description != null)
            {
                this.Visit(definition.Description);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.DecreaseDepth();

            return this;
        }
    }
}