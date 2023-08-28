namespace Chart.Language.SyntaxTree
{
    public static class GraphValueKindExtensions
    {
        /// <summary>
        /// Whether the given <see cref="GraphValueKind" /> is a scalar kind.
        /// </summary>
        /// <remarks>
        /// Variables, lists, objects and enums are not considered scalars.
        /// </remarks>
        public static bool IsScalar(this GraphValueKind kind) =>
            kind switch
            {
                GraphValueKind.Int => true,
                GraphValueKind.Float => true,
                GraphValueKind.String => true,
                GraphValueKind.Boolean => true,

                _ => false
            };
    }
}