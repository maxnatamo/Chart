using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphArgumentsDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphArgumentsDefinition definition)
        {
            if(!definition.Arguments.Any())
            {
                return this;
            }

            this.WriteLine("(");
            this.IncreaseDepth();

            definition.Arguments.ForEach(v => this.Visit(v));

            this.DecreaseDepth();
            this.Write(")");

            return this;
        }

        /// <summary>
        /// Descend into a GraphArgumentDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphArgumentDefinition definition)
        {
            if(definition.Description != null)
            {
                this.Visit(definition.Description);
            }

            this.Write(definition.Name.ToString());
            this.Write(": ");
            this.Write(definition.Type.ToString());

            if(definition.DefaultValue != null)
            {
                this.Write(" = ");
                this.Visit(definition.DefaultValue);
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.WriteLine();

            return this;
        }
    }
}