using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    public class DefinitionNotFoundException : GraphException
    {
        public DefinitionNotFoundException(string definitionName)
            => this.CreateError(definitionName);

        public DefinitionNotFoundException(GraphName definitionName)
            => this.CreateError(definitionName);

        public DefinitionNotFoundException(GraphDefinition definition)
            => this.CreateError(definition.Name);

        private void CreateError(string definitionName)
        {
            this.Error = new ErrorBuilder()
                .SetCode(ErrorCodes.DefinitionNotFound)
                .SetMessage($"Definition '{definitionName}' was not found in the document.")
                .Build();
        }
    }
}