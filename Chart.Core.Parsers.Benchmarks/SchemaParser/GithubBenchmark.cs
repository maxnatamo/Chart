using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using Chart.Models.AST;

namespace Chart.Core.Parsers.Benchmarks
{
    [InProcess()]
    [GcServer(true)]
    [MinColumn, MaxColumn, MemoryDiagnoser]
    public class GithubSchemaParserBenchmark
    {
        [Params("Files/github.graphql")]
        public string Query = "";

        private string Expression = "";
        private List<Token> Tokens = new List<Token>();

        [GlobalSetup]
        public void Setup()
        {
            this.Expression = File.ReadAllText(this.Query);
        }

        [Benchmark]
        public void Tokenize()
        {
            Token token;
            Tokenizer tokenizer = new Tokenizer(this.Expression);

            while((token = tokenizer.GetNextToken()).Type != TokenType.EOF)
            { }
        }

        [Benchmark]
        public GraphDocument Parse()
            => new SchemaParser().Parse(this.Expression);
    }
}
