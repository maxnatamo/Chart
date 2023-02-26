namespace Chart.Core.Parser.Execution
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Document
    {
        private readonly Parser Parser;

        private readonly Serializer Serializer;

        private readonly GraphDocument GraphDocument;

        private readonly SchemaPrinter SchemaPrinter;

        public Document()
        {
            this.Parser = new Parser();
            this.Serializer = new Serializer();
            this.GraphDocument = new GraphDocument();
            this.SchemaPrinter = new SchemaPrinter();
        }

        public string GetSchema()
        {
            return this.SchemaPrinter.Visit(this.GraphDocument).ToString();
        }
    }
}