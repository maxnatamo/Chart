using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <inheritdoc />
    /// <example>
    /// The following example demonstrates how to create a new input field, with type of <c>String!</c>:
    /// <code>
    /// InputFieldDefinition field = new InputFieldDefinition("reason", typeof(NonNullType&#60;StringType&#62;));
    /// </code>
    /// </example>
    public class InputFieldDefinition : ArgumentDefinition
    {
        public InputFieldDefinition(
            string name,
            Type type,
            IGraphValue? defaultValue = null,
            List<Directive>? directives = null)
            : base(name, type, defaultValue, directives)
        {

        }
    }
}