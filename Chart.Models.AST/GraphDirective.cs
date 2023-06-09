namespace Chart.Models.AST
{
    /// <summary>
    /// Definition of a directive in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#Directive">Original documentation</seealso>
    public class GraphDirective : GraphNode
    {
        /// <summary>
        /// The name for the directive.
        /// </summary>
        public GraphName Name { get; set; } = default!;

        /// <summary>
        /// The optional arguments for the directive.
        /// </summary>
        public GraphArguments? Arguments { get; set; } = null;

        /// <summary>
        /// Initialize new GraphDirective-object, without a name.
        /// </summary>
        public GraphDirective()
        { }

        /// <summary>
        /// Initialize new GraphDirective-object, with the specified name.
        /// </summary>
        /// <param name="name">The name of the directive.</param>
        public GraphDirective(string name)
        {
            this.Name = new GraphName(name);
        }

        /// <summary>
        /// Initialize new GraphDirective-object, with the specified name.
        /// </summary>
        /// <param name="name">The name of the directive.</param>
        public GraphDirective(GraphName name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Used with DocumentPrinter.
        /// </summary>
        public override string ToString()
        {
            return "[GraphDirective]";
        }
    }
}