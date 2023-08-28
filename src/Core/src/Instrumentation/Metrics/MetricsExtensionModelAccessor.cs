namespace Chart.Core.Instrumentation
{
    public interface IMetricsExtensionModelAccessor
    {
        MetricsExtensionModel Model { get; }
    }

    public class MetricsExtensionModelAccessor : IMetricsExtensionModelAccessor
    {
        public MetricsExtensionModel Model { get; } = new();
    }
}