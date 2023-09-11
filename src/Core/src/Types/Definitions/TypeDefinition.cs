using Microsoft.Extensions.DependencyInjection;

namespace Chart.Core
{
    public abstract class TypeDefinition
    {
        /// <summary>
        /// In which scope type definitions should be registered in the dependency injection container.
        /// </summary>
        public const ServiceLifetime Scope = ServiceLifetime.Transient;

        /// <summary>
        /// The name of the definition.
        /// </summary>
        public abstract string Name { get; protected set; }

        /// <summary>
        /// An optional description for the definition.
        /// </summary>
        public string? Description { get; set; } = null;

        /// <summary>
        /// An optional runtime type, for the type definition.
        /// </summary>
        public Type? RuntimeType { get; set; } = null;
    }
}