namespace Chart.Language.SyntaxTree
{
    public enum GraphSelectionKind
    {
        /// <summary>
        /// Field selection to use inside a query.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#Field">Original documentation</seealso>
        Field,

        /// <summary>
        /// Fragment selection to use inside a query.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#FragmentSpread">Original documentation</seealso>
        Fragment,

        /// <summary>
        /// Definition of an inline fragment in a selection.
        /// </summary>
        /// <seealso href="https://spec.graphql.org/October2021/#InlineFragment">Original documentation</seealso>
        InlineFragment,
    }
}