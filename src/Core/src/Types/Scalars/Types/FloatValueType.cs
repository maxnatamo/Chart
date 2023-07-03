using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class FloatValueType : ScalarType<double, GraphFloatValue>
    {
        public override double ToLiteral(GraphFloatValue value)
            => value.Value;

        public override GraphFloatValue ToValue(double value)
            => new GraphFloatValue(value);
    }
}