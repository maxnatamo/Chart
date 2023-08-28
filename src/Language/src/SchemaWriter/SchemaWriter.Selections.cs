using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphSelectionSet-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphSelectionSet definition)
        {
            this.WriteLine("{");
            this.IncreaseDepth();

            definition.Selections.ForEach(v => this.Visit(v));

            this.DecreaseDepth();
            this.WriteLine("}");

            return this;
        }

        /// <summary>
        /// Descend into a GraphSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphSelection definition)
        {
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

            return this;
        }
    }
}