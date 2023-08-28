namespace Chart.Language.SyntaxTree
{
    /// <summary>
    /// Definition of a field-list of an input object in the GraphQL-document.
    /// </summary>
    /// <seealso href="https://spec.graphql.org/October2021/#InputFieldsDefinition">Original documentation</seealso>
    public class GraphInputFieldsDefinition : GraphArgumentsDefinition
    {
        /// <inheritdoc />
        public override string ToString() => "[GraphInputFieldsDefinition]";
    }
}