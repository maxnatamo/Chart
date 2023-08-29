using Chart.Language.SyntaxTree;

namespace Chart.Utilities
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphDirectiveDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphDirectiveDefinition definition)
        {
            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            if(definition.Repeatable)
            {
                this.WriteLine($"[GraphValue] Repeatable");
            }

            this.Visit(definition.Locations);

            return this;
        }

        /// <summary>
        /// Descend into a GraphDirectiveLocations-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphDirectiveLocations definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            foreach(GraphDirectiveLocationFlags location in Enum.GetValues<GraphDirectiveLocationFlags>())
            {
                if(!definition.Locations.HasFlag(location))
                {
                    continue;
                }

                this.WriteLine($"[GraphName] {location.ToString()}");
            }
            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphDirectives-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphDirectives definition)
        {
            if(!definition.Directives.Any())
            {
                return this;
            }

            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Directives.ForEach(v => this.Visit(v));
            this.DecreaseDepth();

            return this;
        }

        /// <summary>
        /// Descend into a GraphDirective-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public DocumentPrinter Visit(GraphDirective definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            this.WriteLine($"[GraphName] {definition.Name.ToString()}");

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            this.DecreaseDepth();

            return this;
        }
    }
}