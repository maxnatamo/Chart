using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <inheritdoc />
    /// <example>
    /// The following example demonstrates how to create a new argument, with type of <c>String!</c>:
    /// <code>
    /// ArgumentDefinition argument = new ArgumentDefinition&#60;NonNullType&#60;StringType&#62;&#62;("reason");
    /// </code>
    /// </example>
    public class ArgumentDefinition<TType> : ArgumentDefinition
        where TType : TypeDefinition, new()
    {
        public ArgumentDefinition(
            string name,
            IGraphValue? defaultValue = null,
            List<Directive>? directives = null)
            : base(name, typeof(TType), defaultValue, directives)
        { }
    }
}