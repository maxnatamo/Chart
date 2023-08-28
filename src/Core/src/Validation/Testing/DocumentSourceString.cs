namespace Chart.Core.Validation
{
    public class DocumentSourceString : SchemaSourceString
    {
        public string Query { get; set; } = string.Empty;

        public DocumentSourceString()
            : base(string.Empty)
        { }

        public DocumentSourceString(string query, string? schema = null)
            : base(schema ?? string.Empty)
        {
            this.Query = query;
        }
    }
}