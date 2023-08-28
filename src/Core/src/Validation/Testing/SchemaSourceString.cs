namespace Chart.Core.Validation
{
    public class SchemaSourceString
    {
        public string Schema { get; set; } = string.Empty;

        public SchemaSourceString()
        { }

        public SchemaSourceString(string schema)
        {
            this.Schema = schema;
        }
    }
}