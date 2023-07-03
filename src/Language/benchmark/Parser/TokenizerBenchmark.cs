using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace Chart.Language.Parsers.Benchmarks
{
    [InProcess]
    [GcServer(true)]
    [MinColumn, MaxColumn, MemoryDiagnoser]
    public class SchemaTokenizerBenchmark : BaseBenchmark
    {
        [ParamsSource(nameof(GetGraphTestFiles))]
        public string QueryFile = "";

        private string queryFileContent = "";
        private Tokenizer tokenizer = new Tokenizer();

        [GlobalSetup]
        public void Setup()
        {
            this.queryFileContent = this.ReadTestFile(this.QueryFile);
            this.tokenizer = new Tokenizer(this.queryFileContent);
        }

        [Benchmark]
        public uint Tokenize()
        {
            uint tokenCount = 0;

            Tokenizer tokenizer = new Tokenizer(this.queryFileContent);

            while(tokenizer.GetNextToken().Type != TokenType.EOF)
            {
                tokenCount++;
            }

            return tokenCount;
        }
    }
}