using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class StringValueType : ScalarType<string, GraphStringValue>
    {
        public override string ToLiteral(GraphStringValue value)
            => value.Value;

        public override GraphStringValue ToValue(string value)
            => new GraphStringValue(value);
    }
}