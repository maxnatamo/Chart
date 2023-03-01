using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphDirectiveDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphDirectiveDefinition definition)
        {
            this.WriteLine("directive @");

            if(definition.Name != null)
            {
                this.Write(definition.Name.ToString());
            }

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            if(definition.Repeatable)
            {
                this.Write(" repeatable");
            }

            this.Write(" on ");

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
                this.Write(definition.Locations[i].ToString());

                if(i < definition.Locations.Count - 1)
                {
                    this.Write(" | ");
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
            this.Write($" @{definition.Name.ToString()}");

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }
        }
    }
}