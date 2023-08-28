namespace Chart.Language.SyntaxTree
{
    public interface IHasType
    {
        /// <summary>
        /// The name of the definition.
        /// </summary>
        public string TypeName { get; }
    }
}