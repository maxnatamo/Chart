namespace Chart.Core
{
    public interface IListType : ITypeDefinition
    { }

    public class ListType<TType> : IListType
        where TType : ITypeDefinition
    {
        public string Name => string.Empty;

        public string? Description => null;

        public Type? RuntimeType { get; set; } = null;
    }
}