using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

using Chart.Language.SyntaxTree;

namespace Chart.Language.Parsers.Benchmarks
{
    [InProcess]
    [GcServer(true)]
    [MinColumn, MaxColumn, MemoryDiagnoser]
    public class SchemaParserBenchmark : BaseBenchmark
    {
        [ParamsSource(nameof(GetGraphTestFiles))]
        public string QueryFile = "";

        private string queryFileContent = "";

        [GlobalSetup]
        public void Setup()
        {
            this.queryFileContent = this.ReadTestFile(this.QueryFile);
        }

        [Benchmark]
        public GraphDocument Parse()
            => new SchemaParser().ParseSchema(this.queryFileContent);
    }
}