namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a union member a union definition.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#UnionMemberTypes">Original documentation</seealso>
    public class GraphUnionMembers : GraphNode
    {
        /// <summary>
        /// List of members in the union
        /// </summary>
        public List<GraphName> Members { get; set; } = new List<GraphName>();

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return $"[GraphUnionMembers]";
        }
    }
}