namespace Chart.Core.Parser
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphSelectionSet-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSelectionSet definition)
        {
            this.context.WriteLine(definition.ToString());
            this.context.Descend();

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            definition.Selections.ForEach(this.Visit);
            
            this.context.Ascend();
        }

        /// <summary>
        /// Descend into a GraphSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSelection definition)
        {
            this.context.WriteLine(definition.ToString());

            this.context.Descend();

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

            this.context.Ascend();
        }
    }
}