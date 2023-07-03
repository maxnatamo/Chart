using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class BooleanValueType : ScalarType<bool, GraphBooleanValue>
    {
        public override bool ToLiteral(GraphBooleanValue value)
            => value.Value;

        public override GraphBooleanValue ToValue(bool value)
            => new GraphBooleanValue(value);
    }
}