namespace Chart.Language.SyntaxTree.Tests.SchemaWriterTests
{
    public class VisitGraphDirectiveTests
    {
        [Fact]
        public void Visit_DoesntPrintsDirectives_GivenEmptyDirectiveList()
        {
            new SchemaWriter()
                .Visit(new GraphDirectives())
                .ToString()
                .Should()
                .BeEmpty();
        }

        [Fact]
        public void Visit_PrintsDirectives()
        {
            new SchemaWriter()
                .Visit(new GraphDirectives()
                {
                    Directives = new()
                    {
                        new GraphDirective()
                        {
                            Name = new GraphName("authorize")
                        }
                    }
                })
                .ToString()
                .MatchSnapshot();
        }
    }
}