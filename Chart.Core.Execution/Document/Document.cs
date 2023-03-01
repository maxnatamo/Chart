using Chart.Core.Parsers;
using Chart.Models.AST;

namespace Chart.Core.Execution
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Document
    {
        private readonly SchemaParser Parser;

        private readonly ModelParser ModelParser;

        private readonly GraphDocument GraphDocument;

        private readonly SchemaWriter SchemaWriter;

        public Document()
        {
            this.Parser = new SchemaParser();
            this.ModelParser = new ModelParser();
            this.GraphDocument = new GraphDocument();
            this.SchemaWriter = new SchemaWriter();
        }

        public string GetSchema()
        {
            return this.SchemaWriter.Visit(this.GraphDocument).ToString();
        }
    }
}