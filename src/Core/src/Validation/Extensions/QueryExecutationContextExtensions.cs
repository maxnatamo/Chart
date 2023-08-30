using System.Diagnostics.CodeAnalysis;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public static partial class QueryExecutionContextExtensions
    {
        /// <summary>
        /// Resolve all directive definitions from the schema.
        /// </summary>
        /// <param name="context">The context to get definitions from.</param>
        /// <param name="name">An optional name filter, for the directives.</param>
        public static IEnumerable<GraphDirectiveDefinition> GetDirectiveDefinitions(
            this QueryExecutionContext context,
            string? name = null)
            => context.Schema.GetDefinitions<GraphDirectiveDefinition>(name);

        /// <inheritdoc cref="QueryExecutionContextExtensions.GetDirectiveDefinitions(QueryExecutionContext, string?)" />
        /// <param name="location">Filter for the locations of the directive.</param>
        public static IEnumerable<GraphDirectiveDefinition> GetDirectiveDefinitions(
            this QueryExecutionContext context,
            string? name,
            GraphDirectiveLocationFlags location)
            => context.GetDirectiveDefinitions(name)
                .Where(v => v.Locations.Locations.HasFlag(location));

        /// <summary>
        /// Try to resolve a directive definition from the schema.
        /// </summary>
        /// <param name="context">The context to get definitions from.</param>
        /// <param name="name">An optional name filter, for the directive.</param>
        public static bool TryGetDirectiveDefinition(
            this QueryExecutionContext context,
            string? name,
            [NotNullWhen(true)] out GraphDirectiveDefinition? directiveDefinition)
            => context.Schema.TryGetDefinition<GraphDirectiveDefinition>(name, out directiveDefinition);
    }
}