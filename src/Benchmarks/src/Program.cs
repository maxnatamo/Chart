using BenchmarkDotNet.Running;

namespace Chart.Benchmarks
{
    public static class Program
    {
        private static readonly Type[] projectBenchmarks = new Type[]
        {
            typeof(Chart.Language.Parsers.Benchmarks.SchemaTokenizerBenchmark),
            typeof(Chart.Language.Parsers.Benchmarks.SchemaParserBenchmark),
        };

        public static void Main(string[] args) => BenchmarkSwitcher.FromTypes(Program.projectBenchmarks).Run(args);
    }
}