using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphSelectionSet-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSelectionSet definition)
        {
            this.WriteLine(definition.ToString());
            this.IncreaseDepth();

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            definition.Selections.ForEach(this.Visit);
            
            this.DecreaseDepth();
        }

        /// <summary>
        /// Descend into a GraphSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSelection definition)
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
                    throw new NotImplementedException(definition.SelectionKind.ToString());
            }

            this.DecreaseDepth();
        }
    }
}