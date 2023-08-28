using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphSchemaDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphSchemaDefinition definition)
        {
            this.Write(Keywords.Schema);
            this.WriteSpace();

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Values);

            return this;
        }

        /// <summary>
        /// Descend into a GraphSchemaValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphSchemaValues definition)
        {
            this.WriteLine("{");
            this.IncreaseDepth();

            definition.Values.ForEach(v => this.Visit(v));

            this.DecreaseDepth();
            this.WriteLine("}");

            return this;
        }

        /// <summary>
        /// Descend into a GraphSchemaValue-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphSchemaValue definition)
        {
            this.Write(definition.Operation.ToString());
            this.Write(": ");
            this.WriteLine(definition.Type.ToString());

            return this;
        }
    }
}