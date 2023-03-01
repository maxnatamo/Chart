using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphVariables-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphVariables definition)
        {
            if(!definition.Variables.Any())
            {
                return;
            }

            this.WriteLine("(");
            this.IncreaseDepth();

            definition.Variables.ForEach(this.Visit);

            this.DecreaseDepth();
            this.WriteLine(")");
        }

        /// <summary>
        /// Descend into a GraphVariable-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphVariable definition)
        {
            this.WriteLine($"${definition.Name.Value} : {definition.Type.ToString()}");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }
        }
    }
}