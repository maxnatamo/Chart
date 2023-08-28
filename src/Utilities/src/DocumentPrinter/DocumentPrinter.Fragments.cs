using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphFragmentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphFragmentDefinition definition)
        {
            this.WriteLine($"[GraphType] {definition.Type.ToString()}");

            this.Visit(definition.SelectionSet);

            return this;
        }

        /// <summary>
        /// Descend into a GraphFragmentSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphFragmentSelection definition)
        {
            this.WriteLine($"[GraphName] {definition.Name.ToString()}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphInlineFragmentSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphInlineFragmentSelection definition)
        {
            if(definition.TypeCondition != null)
            {
                this.WriteLine($"[GraphType] {definition.TypeCondition.ToString()}");
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.SelectionSet);

            return this;
        }
    }
}