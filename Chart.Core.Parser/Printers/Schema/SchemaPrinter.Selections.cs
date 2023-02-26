namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphSelectionSet-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphSelectionSet definition)
        {
            this.context.WriteLine("{");
            this.context.Descend();

            definition.Selections.ForEach(this.Visit);

            this.context.Ascend();
            this.context.WriteLine("}");
        }

        /// <summary>
        /// Descend into a GraphSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphSelection definition)
        {
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