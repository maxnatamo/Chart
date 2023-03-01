using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;

namespace Chart.Core.Parsers.Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<GithubSchemaParserBenchmark>();
        }
    }
}