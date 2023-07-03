namespace Chart.Core
{
    [Flags]
    public enum NameFormattingBehaviour
    {
        /// <summary>
        /// Transform all type- and property names to have lowercased first letter (pascal-case).
        /// </summary>
        PascalCase          = 1,

        /// <summary>
        /// Remove 'Get'-prefix from query names.
        /// </summary>
        RemoveGetPrefix     = 2,

        /// <summary>
        /// Remove 'Async'-postfix from query names.
        /// </summary>
        RemoveAsyncPostfix  = 4,
    }
}