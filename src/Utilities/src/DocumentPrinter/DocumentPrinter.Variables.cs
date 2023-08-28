using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphVariables-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphVariables definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Variables.ForEach(v => this.Visit(v));
            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphVariable-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphVariable definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            this.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.WriteLine($"[GraphType] {definition.Type.ToString()}");

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