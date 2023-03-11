using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

using Chart.Shared.Exporters;
using Chart.Models.AST;

namespace Chart.Core.Parsers.Benchmarks
{
    [InProcess]
    [GcServer(true)]
    [MinColumn, MaxColumn, MemoryDiagnoser]
    [OpenMetricsExporter]
    public class SchemaParserBenchmark : BaseBenchmark
    {
        [ParamsSource(nameof(GetGraphTestFiles))]
        public string QueryFile = "";

        private string Expression = "";
        private List<Token> Tokens = new List<Token>();

        [GlobalSetup]
        public void Setup()
        {
            this.Expression = this.ReadTestFile(this.QueryFile);
        }

        [Benchmark]
        public uint Tokenize()
        {
            uint tokenCount = 0;

            Tokenizer tokenizer = new Tokenizer(this.Expression);

            while(tokenizer.GetNextToken().Type != TokenType.EOF)
            {
                tokenCount++;
            }

            return tokenCount;
        }

        [Benchmark]
        public GraphDocument Parse()
            => new SchemaParser().Parse(this.Expression);
    }
}
