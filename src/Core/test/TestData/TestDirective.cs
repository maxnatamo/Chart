using Chart.Language.SyntaxTree;
using static Chart.Language.SyntaxTree.GraphDirectiveLocationFlags;

namespace Chart.Core.Tests
{
    public class TestDirective : DirectiveDefinition
    {
        public TestDirective()
            : base("TestDirective", OBJECT | SCALAR)
        { }
    }
}