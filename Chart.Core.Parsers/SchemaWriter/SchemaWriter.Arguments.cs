using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphArguments-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphArguments definition)
        {
            if(!definition.Arguments.Any())
            {
                return;
            }

            this.Write("(");
            definition.Arguments.ForEach(this.Visit);
            this.Write(")");
        }

        /// <summary>
        /// Descend into a GraphArgument-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphArgument definition)
        {
            this.Write($"{definition.Name.ToString()}: ");
            this.Visit(definition.Value);
        }
    }
}