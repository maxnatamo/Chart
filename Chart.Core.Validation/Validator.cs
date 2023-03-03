using Chart.Models.AST;

namespace Chart.Core.Validation
{
    public partial class Validator
    {
        public Validator()
        {

        }

        public bool TryValidate(GraphDocument document)
        {
            // The validator only validates executable expressions, as per the spec.
            // https://spec.graphql.org/October2021/#sel-GALNDFFCAACCa7-V
            if(document.Definitions.Any(v => !v.Executable))
            {
                return false;
            }

            for(int i = 0; i < document.Definitions.Count; i++)
            {
                if(!this.Validate(document, document.Definitions[i]))
                {
                    return false;
                }
            }

            return true;
        }

        protected bool Validate(GraphDocument document, GraphDefinition definition) => definition.DefinitionKind switch
        {
            GraphDefinitionKind.Operation => this.ValidateOperation(document, (GraphOperationDefinition) definition),
            GraphDefinitionKind.Fragment  => this.ValidateFragment(document, (GraphFragmentDefinition) definition),
            _ => false
        };
    }
}