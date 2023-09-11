namespace Chart.Core
{
    public abstract class NonNullType : TypeDefinition
    { }

    public class NonNullType<TType> : NonNullType
        where TType : TypeDefinition
    {
        public override string Name { get; protected set; } = string.Empty;
    }
}