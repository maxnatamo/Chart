using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Chart.Core.Reflection.Benchmarks
{
    [InProcess()]
    [GcServer(true)]
    [MinColumn, MaxColumn, MemoryDiagnoser]
    public class DynamicBenchmark
    {
        private ReflectedType ReflectedType { get; set; } = default!;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.ReflectedType = new ReflectedType();
            this.ReflectedType.SetValue("Key", 1);
        }

        [Benchmark(Baseline = true)]
        public object? GetValue()
        {
            return this.ReflectedType.GetValue("Key");
        }

        [Benchmark]
        public object SetValue()
        {
            this.ReflectedType.SetValue("Key", 1);
            return this.ReflectedType;
        }

        [Benchmark]
        public object Remove()
        {
            this.ReflectedType.Remove("Key");
            return this.ReflectedType;
        }
    }
}
