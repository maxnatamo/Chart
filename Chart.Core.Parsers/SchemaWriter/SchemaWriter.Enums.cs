using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphEnumDefinition-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphEnumDefinition definition)
        {
            this.Write("enum ");

            if(definition.Name != null)
            {
                this.WriteLine(definition.Name.ToString() + " ");
            }

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }

            this.Visit(definition.Values);
        }

        /// <summary>
        /// Descend into a GraphEnumDefinitionValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphEnumDefinitionValues definition)
        {
            this.WriteLine("{");
            this.IncreaseDepth();

            definition.Values.ForEach(this.Visit);

            this.DecreaseDepth();
            this.WriteLine("}");
        }

        /// <summary>
        /// Descend into a GraphEnumDefinitionValues-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        public void Visit(GraphEnumDefinitionValue definition)
        {
            if(definition.Description != null)
            {
                this.Visit(definition.Description);
            }

            this.WriteLine(definition.Name.ToString());

            if(definition.Directives != null)
            {
                this.Visit(definition.Directives);
            }
        }
    }
}