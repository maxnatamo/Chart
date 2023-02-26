using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;

namespace Chart.Core.Parser.Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<GithubParserBenchmark>();
        }
    }
}