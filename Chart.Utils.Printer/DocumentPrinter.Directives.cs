using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphDirectiveDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDirectiveDefinition definition)
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
        }

        /// <summary>
        /// Descend into a GraphDirectiveLocations-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDirectiveLocations definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            definition.Locations.ForEach(v =>
            {
                this.WriteLine($"[GraphName] {v.Value}");
            });
            this.DecreaseDepth();
        }

        /// <summary>
        /// Descend into a GraphDirectives-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDirectives definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            definition.Directives.ForEach(this.Visit);

            this.DecreaseDepth();
        }

        /// <summary>
        /// Descend into a GraphDirective-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDirective definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();
            
            this.WriteLine($"[GraphName] {definition.Name.ToString()}");

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            this.DecreaseDepth();
        }
    }
}