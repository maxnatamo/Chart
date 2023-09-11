using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <inheritdoc />
    /// <example>
    /// The following example demonstrates how to create a new input field, with type of <c>String!</c>:
    /// <code>
    /// InputFieldDefinition field = new InputFieldDefinition&#60;NonNullType&#60;StringType&#62;&#62;("reason");
    /// </code>
    /// </example>
    public class InputFieldDefinition<TType> : InputFieldDefinition
        where TType : TypeDefinition, new()
    {
        public InputFieldDefinition(
            string name,
            IGraphValue? defaultValue = null,
            List<Directive>? directives = null)
            : base(name, typeof(TType), defaultValue, directives)
        { }
    }
}