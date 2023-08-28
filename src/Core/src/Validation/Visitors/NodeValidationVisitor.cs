using System.Runtime.CompilerServices;

using Chart.Language.SyntaxTree;

namespace Chart.Core
{
    /// <summary>
    /// A node visitor which executes, when matching the type parameter.
    /// </summary>
    public class NodeValidationVisitor<TNode, TContext> : IValidationVisitor
        where TNode : IGraphNode
        where TContext : ValidationContext
    {
        private readonly Func<TNode, TContext, Task>? _enter = null;
        private readonly Func<TNode, TContext, Task>? _leave = null;

        public NodeValidationVisitor(
            Func<TNode, TContext, Task>? enter = null,
            Func<TNode, TContext, Task>? leave = null)
        {
            if(enter == null && leave == null)
            {
                throw new ArgumentException("No visitor action was defined. Add an enter or leave action to the visitor.");
            }

            this._enter = enter;
            this._leave = leave;
        }

        /// <inheritdoc />
        public async Task EnterAsync(IGraphNode node, ValidationContext context)
        {
            if(this._enter is not null && node is TNode _node && context is TContext _context)
            {
                await this._enter(_node, _context);
            }
        }

        /// <inheritdoc />
        public async Task LeaveAsync(IGraphNode node, ValidationContext context)
        {
            if(this._leave is not null && node is TNode _node && context is TContext _context)
            {
                await this._leave(_node, _context);
            }
        }
    }
}