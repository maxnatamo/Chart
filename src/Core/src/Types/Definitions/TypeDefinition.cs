namespace Chart.Core
{
    public interface ITypeDefinition
    {
        /// <summary>
        /// The name of the definition.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// An optional description for the definition.
        /// </summary>
        string? Description { get; }

        /// <summary>
        /// An optional runtime type, for the type definition.
        /// </summary>
        Type? RuntimeType { get; set; }
    }

    public abstract class TypeDefinition : ITypeDefinition
    {
        /// <inheritdoc />
        public abstract string Name { get; protected set; }

        /// <inheritdoc />
        public string? Description { get; set; } = null;

        /// <inheritdoc />
        public Type? RuntimeType { get; set; } = null;
    }
}