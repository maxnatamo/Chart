using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphOperationDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphOperationDefinition definition)
        {
            if(definition.Variables != null)
            {
                this.Visit(definition.Variables);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Selections);

            return this;
        }
    }
}