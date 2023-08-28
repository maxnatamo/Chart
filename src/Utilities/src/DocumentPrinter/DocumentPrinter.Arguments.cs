using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphArguments-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphArguments definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Arguments.ForEach(v => this.Visit(v));
            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphArgument-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphArgument definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            this.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.Visit(definition.Value);

            this.DecreaseDepth();

            return this;
        }
    }
}