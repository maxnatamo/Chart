namespace Chart.Core
{
    [Flags]
    public enum NameFormattingBehaviour
    {
        /// <summary>
        /// Remove 'Get'-prefix from query names.
        /// </summary>
        RemoveGetPrefix = 1,

        /// <summary>
        /// Remove 'Async'-postfix from query names.
        /// </summary>
        RemoveAsyncPostfix = 2,

        /// <summary>
        /// Transform all type- and property names to have lowercased first letter (pascal-case).
        /// </summary>
        PascalCase = 4,

        /// <summary>
        /// Transform all type- and property names to have uppercased first letter (camel-case). Negates <see cref="PascalCase" />.
        /// </summary>
        CamelCase = 8,
    }
}