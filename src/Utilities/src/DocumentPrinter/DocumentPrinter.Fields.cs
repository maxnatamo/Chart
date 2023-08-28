using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphFieldSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphFieldSelection definition)
        {
            this.WriteLine($"[GraphName] {definition.Name.ToString()}");

            if(definition.Alias is not null)
            {
                this.WriteLine($"[GraphAlias] {definition.Alias.ToString()}");
            }

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.SelectionSet != null)
            {
                this.Visit(definition.SelectionSet);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphFields-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphFields definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            definition.Fields.ForEach(v => this.Visit(v));

            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphField-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphField definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            this.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.WriteLine($"[GraphType] {definition.Type.ToString()}");

            if(definition.Description != null)
            {
                this.Visit(definition.Description);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            this.DecreaseDepth();

            return this;
        }
    }
}