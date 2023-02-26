namespace Chart.Core.Parser
{
    public partial class SchemaPrinter
    {
        /// <summary>
        /// Descend into a GraphDirectiveDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphDirectiveDefinition definition)
        {
            this.context.WriteLine("directive @");

            if(definition.Name != null)
            {
                this.context.Write(definition.Name.ToString());
            }

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            if(definition.Repeatable)
            {
                this.context.Write(" repeatable");
            }

            this.context.Write(" on ");

            this.Visit(definition.Locations);
        }

        /// <summary>
        /// Descend into a GraphDirectives-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphDirectiveLocations definition)
        {
            for(int i = 0; i < definition.Locations.Count; i++)
            {
                this.context.Write(definition.Locations[i].ToString());

                if(i < definition.Locations.Count - 1)
                {
                    this.context.Write(" | ");
                }
            }
        }

        /// <summary>
        /// Descend into a GraphDirectives-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphDirectives definition)
        {
            if(!definition.Directives.Any())
            {
                return;
            }

            definition.Directives.ForEach(this.Visit);
        }

        /// <summary>
        /// Descend into a GraphDirective-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphDirective definition)
        {
            this.context.Write($" @{definition.Name.ToString()}");

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }
        }
    }
}