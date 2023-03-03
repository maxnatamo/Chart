using Chart.Models.AST;

namespace Chart.Core.Validation
{
    public partial class Validator
    {
        /// <summary>
        /// Validate subscription operation in a document.
        /// </summary>
        /// <remarks>
        /// Fragment selections are not validated here. Instead, it's validated on the fragment-definition.
        /// </remarks>
        /// <param name="document">The parent document.</param>
        /// <param name="definition">The operation to validate.</param>
        /// <returns>True, if the document is valid. Otherwise, false.</returns>
        public bool ValidateSubscriptionOperation(GraphDocument document, GraphSubscriptionOperation definition)
            => definition.Selections.Selections.Count() == 1;
    }
}