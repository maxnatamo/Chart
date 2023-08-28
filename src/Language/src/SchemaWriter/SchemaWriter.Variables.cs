using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphVariables-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphVariables definition)
        {
            if(!definition.Variables.Any())
            {
                return this;
            }

            this.WriteLine("(");
            this.IncreaseDepth();

            definition.Variables.ForEach(v => this.Visit(v));

            this.DecreaseDepth();
            this.WriteLine(")");

            return this;
        }

        /// <summary>
        /// Descend into a GraphVariable-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphVariable definition)
        {
            this.WriteLine($"${definition.Name.Value} : {definition.Type.ToString()}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            return this;
        }
    }
}