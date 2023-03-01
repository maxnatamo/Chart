using Chart.Models.AST;

namespace Chart.Utils.Printer
{
    public partial class DocumentPrinter
    {
        /// <summary>
        /// Descend into a GraphFieldSelection-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFieldSelection definition)
        {
            this.WriteLine($"[GraphName] {definition.Name.ToString()}");

            if(definition.Alias != null)
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
        }

        /// <summary>
        /// Descend into a GraphFields-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphFields definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            definition.Fields.ForEach(this.Visit);

            this.DecreaseDepth();
        }

        /// <summary>
        /// Descend into a GraphField-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphField definition)
        {
            this.WriteLine(definition.ToString());

            this.IncreaseDepth();

            this.WriteLine($"[GraphName] {definition.Name.ToString()}");
            this.WriteLine($"[GraphType] {definition.Type.ToString()}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            this.DecreaseDepth();
        }
    }
}