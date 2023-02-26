using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Chart.Core.Parser.Benchmarks
{
    [InProcess()]
    [GcServer(true)]
    [MinColumn, MaxColumn, MemoryDiagnoser]
    public class GithubParserBenchmark
    {
        [Params("Files/github.graphql")]
        public string QueryFile = "";

        private string Query = "";
        private List<Token> Tokens = new List<Token>();

        [GlobalSetup]
        public void Setup()
        {
            this.Query = File.ReadAllText(this.QueryFile);
            this.Tokens = new Tokenizer(this.Query).GetAllTokens();
        }

        [Benchmark]
        public List<Token> Tokenize()
            => new Tokenizer(this.Query).GetAllTokens();

        [Benchmark]
        public GraphDocument Parse()
            => new Parser().Parse(this.Query, this.Tokens);
    }
}
