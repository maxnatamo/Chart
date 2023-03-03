using Chart.Models.AST;

namespace Chart.Core.Validation
{
    public partial class Validator
    {
        /// <summary>
        /// Validate all operations in a document.
        /// </summary>
        /// <param name="document">The parent document.</param>
        /// <param name="definition">The operation to validate.</param>
        /// <returns>True, if the document is valid. Otherwise, false.</returns>
        public bool ValidateOperation(GraphDocument document, GraphOperationDefinition definition)
        {
            // Validate named operations
            if(definition.Name != null)
            {
                if(!this.ValidateNamedOperation(document, definition))
                {
                    return false;
                }
            }
            else
            {
                if(!this.ValidateAnonymousOperation(document, definition))
                {
                    return false;
                }
            }

            if(definition.OperationKind == GraphOperationKind.Subscription)
            {
                if(!this.ValidateSubscriptionOperation(document, (GraphSubscriptionOperation) definition))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validate named operation in a document.
        /// </summary>
        /// <param name="document">The parent document.</param>
        /// <param name="definition">The operation to validate.</param>
        /// <returns>True, if the document is valid. Otherwise, false.</returns>
        public bool ValidateNamedOperation(GraphDocument document, GraphOperationDefinition definition)
            => document.Definitions
                .Where(v => v.DefinitionKind == GraphDefinitionKind.Operation && v.Name != null && v.Name.Equals(definition.Name))
                .Count() <= 1;

        /// <summary>
        /// Validate anonymous operation in a document.
        /// </summary>
        /// <param name="document">The parent document.</param>
        /// <param name="definition">The operation to validate.</param>
        /// <returns>True, if the document is valid. Otherwise, false.</returns>
        public bool ValidateAnonymousOperation(GraphDocument document, GraphOperationDefinition definition)
        {
            var operations = document.Definitions.Where(v => v.DefinitionKind == GraphDefinitionKind.Operation);

            // https://spec.graphql.org/October2021/#sel-IALPFDDDFCAAEDBBPrxQ
            if(operations.Any(v => v.Name == null))
            {
                return operations.Count() <= 1;
            }

            return true;
        }
    }
}