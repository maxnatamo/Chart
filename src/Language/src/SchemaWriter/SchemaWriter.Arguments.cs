using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphArguments-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphArguments definition)
        {
            if(!definition.Arguments.Any())
            {
                return this;
            }

            this.Write("(");
            this.WriteMany(definition.Arguments, v => this.Visit(v), ", ");
            this.Write(")");

            return this;
        }

        /// <summary>
        /// Descend into a GraphArgument-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphArgument definition)
        {
            this.Write(definition.Name.ToString());
            this.Write(": ");
            this.Visit(definition.Value);

            return this;
        }
    }
}