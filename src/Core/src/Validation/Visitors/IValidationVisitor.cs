using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// Contract for handling a single validation round on a node.
    /// </summary>
    public interface IValidationVisitor
    {
        /// <summary>
        /// <para>
        /// Validate the given node, using the given validation context.
        /// </para>
        /// <para>
        /// This method is executed before document fields have been collected.
        /// </para>
        /// </summary>
        /// <param name="node">The node to validate.</param>
        /// <param name="context">The validation context.</param>
        Task EnterAsync(IGraphNode node, ValidationContext context);

        /// <summary>
        /// <para>
        /// Validate the given node, using the given validation context.
        /// </para>
        /// <para>
        /// This method is executed after document fields have been collected.
        /// </para>
        /// </summary>
        /// <param name="node">The node to validate.</param>
        /// <param name="context">The validation context.</param>
        Task LeaveAsync(IGraphNode node, ValidationContext context);
    }
}