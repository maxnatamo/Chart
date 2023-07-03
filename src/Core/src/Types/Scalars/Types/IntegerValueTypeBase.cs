using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public abstract class IntegerValueTypeBase<TIntegerType> : ScalarType<TIntegerType, GraphIntValue>
        where TIntegerType : IComparable
    {
        protected abstract TIntegerType MinValue { get; }
        protected abstract TIntegerType MaxValue { get; }

        public override bool IsValid(TIntegerType value)
            => value.CompareTo(MinValue) >= 0 && value.CompareTo(MaxValue) <= 0;
    }
}