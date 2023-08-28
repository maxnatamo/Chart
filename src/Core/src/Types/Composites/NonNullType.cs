namespace Chart.Core
{
    public interface INonNullType : ITypeDefinition
    { }

    public class NonNullType<TType> : INonNullType
        where TType : ITypeDefinition
    {
        public string Name => string.Empty;

        public string? Description => null;

        public Type? RuntimeType { get; set; } = null;
    }
}