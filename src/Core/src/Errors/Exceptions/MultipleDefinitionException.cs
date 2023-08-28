using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class MultipleDefinitionException : GraphException
    {
        public MultipleDefinitionException(string definitionName)
            => this.CreateError(definitionName);

        public MultipleDefinitionException(GraphName definitionName)
            => this.CreateError(definitionName);

        public MultipleDefinitionException(GraphDefinition definition)
            => this.CreateError(definition.Name);

        private void CreateError(string definitionName)
        {
            this.Error = new ErrorBuilder()
                .SetCode(ErrorCodes.AmbiguousDefinition)
                .SetMessage($"Ambiguous definition '{definitionName}' was found in the schema.")
                .Build();
        }
    }
}