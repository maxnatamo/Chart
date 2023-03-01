using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphSchemaDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphSchemaDefinition definition)
        {
            this.Write("schema ");

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Values);
        }

        /// <summary>
        /// Descend into a GraphSchemaValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSchemaValues definition)
        {
            this.WriteLine("{");
            this.IncreaseDepth();

            definition.Values.ForEach(this.Visit);

            this.DecreaseDepth();
            this.WriteLine("}");
        }

        /// <summary>
        /// Descend into a GraphSchemaValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphSchemaValue definition)
        {
            this.Write(definition.Operation.ToString());
            this.Write(": ");
            this.WriteLine(definition.Type.ToString());
        }
    }
}