using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphEnumDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphEnumDefinition definition)
        {
            this.Write(Keywords.Enum);
            this.WriteSpace();

            this.Write(definition.Name.ToString());
            this.WriteSpace();

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            if(definition.Values != null)
            {
                this.Visit(definition.Values);
            }

            return this;
        }

        /// <summary>
        /// Descend into a GraphEnumDefinitionValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphEnumDefinitionValues definition)
        {
            this.WriteLine("{");
            this.IncreaseDepth();

            definition.Values.ForEach(v => this.Visit(v));

            this.DecreaseDepth();
            this.WriteLine("}");

            return this;
        }

        /// <summary>
        /// Descend into a GraphEnumDefinitionValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public SchemaWriter Visit(GraphEnumDefinitionValue definition)
        {
            if(definition.Description != null)
            {
                this.Visit(definition.Description);
            }

            this.Write(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.WriteLine();

            return this;
        }
    }
}