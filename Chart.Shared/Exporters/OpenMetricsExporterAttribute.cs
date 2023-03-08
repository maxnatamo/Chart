
using BenchmarkDotNet.Attributes;

namespace Chart.Shared.Exporters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true)]
    public class OpenMetricsExporterAttribute : ExporterConfigBaseAttribute
    {
        public OpenMetricsExporterAttribute() : base(new OpenMetricsExporter())
        {
        }
    }
}