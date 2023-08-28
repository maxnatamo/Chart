using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Collection of multiple visitors, which are executed sequentially.
    /// </summary>
    public class ValidationVisitors : IValidationVisitor
    {
        private readonly List<IValidationVisitor> _validationVisitors;

        public ValidationVisitors(params IValidationVisitor[] visitors)
            => this._validationVisitors = visitors.ToList();

        public ValidationVisitors(IEnumerable<IValidationVisitor> visitors)
            => this._validationVisitors = visitors.ToList();

        /// <inheritdoc />
        public async Task EnterAsync(IGraphNode node, ValidationContext context)
        {
            foreach(IValidationVisitor visitor in this._validationVisitors)
            {
                await visitor.EnterAsync(node, context);
            }
        }

        /// <inheritdoc />
        public async Task LeaveAsync(IGraphNode node, ValidationContext context)
        {
            foreach(IValidationVisitor visitor in this._validationVisitors)
            {
                await visitor.LeaveAsync(node, context);
            }
        }
    }
}