using Chart.Models.AST;

namespace Chart.Core.Validation
{
    public partial class Validator
    {
        /// <summary>
        /// Validate fragment definition in a document.
        /// </summary>
        /// <param name="document">The parent document.</param>
        /// <param name="definition">The operation to validate.</param>
        /// <returns>True, if the document is valid. Otherwise, false.</returns>
        public bool ValidateFragment(GraphDocument document, GraphFragmentDefinition definition)
        {
            if(definition.Name == null)
            {
                return false;
            }

            // https://spec.graphql.org/October2021/#example-18466
            if(definition.Type.Name.Value == "Subscription")
            {
                if(definition.SelectionSet.Selections.Count > 1)
                {
                    return false;
                }
            }

            // https://spec.graphql.org/October2021/#sel-FALVDDFDBAAUFFBAAAsyT
            if(document.Definitions.Where(v =>
                v.DefinitionKind == GraphDefinitionKind.Fragment &&
                v.Name != null &&
                v.Name.Equals(definition.Name)).Count() != 1)
            {
                return false;
            }

            return true;
        }
    }
}