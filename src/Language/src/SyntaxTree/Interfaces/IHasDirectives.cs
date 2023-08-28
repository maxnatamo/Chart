namespace Chart.Language.SyntaxTree
{
    public interface IHasDirectives
    {
        /// <summary>
        /// Optional directives.
        /// </summary>
        public GraphDirectives? Directives { get; set; }
    }
}