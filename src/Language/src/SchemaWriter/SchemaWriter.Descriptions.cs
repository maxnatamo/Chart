using Chart.Language.SyntaxTree;

namespace Chart.Language
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphDescription-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected SchemaWriter Visit(GraphDescription definition)
        {
            this.WriteLine("\"\"\"");
            this.WriteLine(definition.Value);
            this.WriteLine("\"\"\"");

            return this;
        }
    }
}