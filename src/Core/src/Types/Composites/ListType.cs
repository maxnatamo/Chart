namespace Chart.Core
{
    public abstract class ListType : TypeDefinition
    { }

    public class ListType<TType> : ListType
        where TType : TypeDefinition
    {
        public override string Name { get; protected set; } = string.Empty;
    }
}