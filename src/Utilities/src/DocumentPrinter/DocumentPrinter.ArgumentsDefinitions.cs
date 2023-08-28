using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphArgumentsDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphArgumentsDefinition definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Arguments.ForEach(v => this.Visit(v));
            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphArgumentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphArgumentDefinition definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            this.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.WriteLine($"[GraphType] {definition.Type.ToString()}");

            if(definition.Description != null)
            {
                this.Visit(definition.Description);
            }

            if(definition.DefaultValue != null)
            {
                this.Visit(definition.DefaultValue);
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