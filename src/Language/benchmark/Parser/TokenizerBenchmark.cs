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

        private Tokenizer Tokenizer = new();

        [GlobalSetup]
        public void Setup()
        {
            string queryFileContent = this.ReadTestFile(this.QueryFile);
            this.Tokenizer = new Tokenizer(queryFileContent);
        }

        [Benchmark]
        public uint Tokenize()
        {
            uint tokenCount = 0;

            while(this.Tokenizer.GetNextToken().Type != TokenType.EOF)
            {
                tokenCount++;
            }

            return tokenCount;
        }
    }
}