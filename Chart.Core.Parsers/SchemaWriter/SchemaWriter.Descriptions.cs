using Chart.Models.AST;

namespace Chart.Core.Parsers
{
    public partial class SchemaWriter
    {
        /// <summary>
        /// Descend into a GraphDescription-object.
        /// </summary>
        /// <param name="definition">The definition to descend into.</param>
        protected void Visit(GraphDescription definition)
        {
            this.WriteLine("\"\"\"");
            this.WriteLine(definition.Value);
            this.WriteLine("\"\"\"");
        }
    }
}