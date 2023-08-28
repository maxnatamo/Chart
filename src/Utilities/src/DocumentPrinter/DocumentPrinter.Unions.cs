using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphUnionDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphUnionDefinition definition)
        {
            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Members != null)
            {
                this.Visit(definition.Members);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphUnionMembers-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphUnionMembers definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Members.ForEach(v =>
            {
                this.WriteLine($"[GraphName] {v.ToString()}");
            });
            this.DecreaseDepth();

            return this;
        }
    }
}