using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphDirectiveDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphDirectiveDefinition definition)
        {
            this.Write(Keywords.Directive);
            this.WriteSpace();

            this.Write("@" + definition.Name.ToString());

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            if(definition.Repeatable)
            {
                this.WriteSpace();
                this.Write(Keywords.Repeatable);
            }

            this.WriteSpace();
            this.Write(Keywords.On);
            this.WriteSpace();

            this.Visit(definition.Locations);

            this.WriteLine();

            return this;
        }

        /// <summary>
        /// Descend into a GraphDirectives-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphDirectiveLocations definition)
        {
            List<GraphDirectiveLocation> locations = Enum.GetValues<GraphDirectiveLocation>().ToList();
            locations = locations.Where(l => definition.Locations.HasFlag(l)).ToList();

            this.WriteMany(locations, v => this.Write(v.ToString()), " | ");

            return this;
        }

        /// <summary>
        /// Descend into a GraphDirectives-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphDirectives definition)
        {
            if(!definition.Directives.Any())
            {
                return this;
            }

            definition.Directives.ForEach(v => this.Visit(v));

            return this;
        }

        /// <summary>
        /// Descend into a GraphDirective-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphDirective definition)
        {
            this.Write($" @{definition.Name}");

            if(definition.Arguments != null)
            {
                this.Visit(definition.Arguments);
            }

            return this;
        }
    }
}