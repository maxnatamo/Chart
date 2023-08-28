using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Context for validating the current schema.
    /// </summary>
    public class SchemaValidationContext : ValidationContext
    {
        public SchemaValidationContext(Schema schema)
            : base(schema)
        { }
    }
}