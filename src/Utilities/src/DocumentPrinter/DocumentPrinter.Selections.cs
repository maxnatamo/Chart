using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphSelectionSet-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphSelectionSet definition)
        {
            this.WriteLine(definition.ToString());
            this.IncreaseDepth();

            definition.Selections.ForEach(v => this.Visit(v));

            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphSelection definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            switch(definition.SelectionKind)
            {
                case GraphSelectionKind.Field:
                    this.Visit((GraphFieldSelection) definition);
                    break;

                case GraphSelectionKind.Fragment:
                    this.Visit((GraphFragmentSelection) definition);
                    break;

                case GraphSelectionKind.InlineFragment:
                    this.Visit((GraphInlineFragmentSelection) definition);
                    break;

                default:
                    throw new NotSupportedException(definition.SelectionKind.ToString());
            }

            this.DecreaseDepth();

            return this;
        }
    }
}