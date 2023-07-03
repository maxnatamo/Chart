namespace Chart.Core
{
    public class TypeDefinition
    {
        /// <summary>
        /// An explcit name for the type.
        /// </summary>
        public string? Name { get; protected set; } = null;

        /// <summary>
        /// An optional description for the type.
        /// </summary>
        public string? Description { get; protected set; } = null;
    }
}