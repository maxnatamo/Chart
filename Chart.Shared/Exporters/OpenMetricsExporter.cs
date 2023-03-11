
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Environments;

namespace Chart.Shared.Exporters
{
    /// <summary>
    /// Header for metrics in OpenMetric.
    /// </summary>
    internal class OpenMetricsHeader
    {
        /// <summary>
        /// The name of the metric.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// The type of metric, defaults to gauge.
        /// </summary>
        public string Type { get; set; } = "gauge";

        /// <summary>
        /// The optional unit of the metric.
        /// The line is skipped, if set to null.
        /// </summary>
        public string? Unit { get; set; } = null;

        /// <summary>
        /// Optional help-text for the metric.
        /// The line is skipped, if set to null.
        /// </summary>
        public string? Help { get; set; } = null;
    }

    /// <summary>
    /// Content of a measurement in OpenMetrics, including the header.
    /// </summary>
    internal class OpenMetricsMeasurement
    {
        /// <summary>
        /// The header of the metric.
        /// </summary>
        public OpenMetricsHeader Header { get; set; } = new OpenMetricsHeader();

        /// <summary>
        /// List of labels for the measurement.
        /// </summary>
        public Dictionary<string, object?> Labels = new Dictionary<string, object?>();

        /// <summary>
        /// The value of the measurement.
        /// </summary>
        public object? Value { get; set; } = null;
    }

    /// <summary>
    /// BenchmarkDotNet exporter for OpenMetrics-format.
    /// </summary>
    /// <remarks>
    /// The motivation for this exporter came from GitLab, with support for metric reports,
    /// using the OpenMetrics-format. These might be useful for testing performance in pull-requests and releases.
    /// </remarks>
    /// <seealso href="https://github.com/OpenObservability/OpenMetrics">Specification link.</seealso>
    public class OpenMetricsExporter : ExporterBase
    {
        /// <summary>
        /// OpenMetrics doesn't seem to have a dedicated suffix, so this will have to do.
        /// </summary>
        protected override string FileExtension => "metrics";

        /// <summary>
        /// Prefix for all metrics exported.
        /// </summary>
        protected string MetricPrefix = "benchmark";

        /// <summary>
        /// Export a Summary-instance into a string-logger.
        /// </summary>
        /// <param name="summary">The Summary-instance to read from.</param>
        /// <param name="logger">The ILogger-instance to export to.</param>
        public override void ExportToLog(Summary summary, ILogger logger)
        {
            foreach(var report in summary.Reports)
            {
                // Create common labels for all reports
                Dictionary<string, object?> labels = new Dictionary<string, object?>
                {
                    { "job", report.BenchmarkCase.Job.ResolvedId },
                    { "namespace", report.BenchmarkCase.Descriptor.Type.Namespace },
                    { "type", report.BenchmarkCase.Descriptor.Type.Name },
                    { "method", report.BenchmarkCase.Descriptor.WorkloadMethod.Name },
                    { "title", report.BenchmarkCase.Descriptor.WorkloadMethodDisplayInfo },
                    { "parameters", report.BenchmarkCase.Parameters.PrintInfo },
                    { "processor", summary.HostEnvironmentInfo.CpuInfo.Value.ProcessorName },
                    { "pcpu", summary.HostEnvironmentInfo.CpuInfo.Value.PhysicalProcessorCount },
                    { "pcore", summary.HostEnvironmentInfo.CpuInfo.Value.PhysicalCoreCount },
                    { "vcore", summary.HostEnvironmentInfo.CpuInfo.Value.LogicalCoreCount },
                    { "arch", summary.HostEnvironmentInfo.Architecture },
                    { "dotnet_version", summary.HostEnvironmentInfo.DotNetSdkVersion.Value },
                };

                this.ExportReport(logger, summary.HostEnvironmentInfo, report, labels);
            }
        }

        /// <summary>
        /// Export an individual report into the specified logger.
        /// </summary>
        /// <param name="logger">The ILogger-instance to export to.</param>
        /// <param name="info">Information about the host environment, from the summary.</param>
        /// <param name="report">The report to export.</param>
        /// <param name="labels">Common labels for all measurements in the report.</param>
        private void ExportReport(ILogger logger, HostEnvironmentInfo info, BenchmarkReport report, Dictionary<string, object?> labels)
        {
            List<OpenMetricsMeasurement> metrics = new List<OpenMetricsMeasurement>
            {
                new OpenMetricsMeasurement
                {
                    Header = new OpenMetricsHeader
                    {
                        Name = $"{this.MetricPrefix}_duration_avg",
                        Unit = "nanoseconds",
                        Help = "Average/mean of all measurements in nanoseconds"
                    },
                    Labels = labels,
                    Value = report.AllMeasurements.Average(v => v.Nanoseconds)
                },
                new OpenMetricsMeasurement
                {
                    Header = new OpenMetricsHeader
                    {
                        Name = $"{this.MetricPrefix}_duration_median",
                        Unit = "nanoseconds",
                        Help = "Median of all measurements in nanoseconds"
                    },
                    Labels = labels,
                    Value = report.AllMeasurements.OrderBy(v => v.Nanoseconds).ToList()[(int) Math.Floor(report.AllMeasurements.Count / 2.0f)]
                },
                new OpenMetricsMeasurement
                {
                    Header = new OpenMetricsHeader
                    {
                        Name = $"{this.MetricPrefix}_duration_min",
                        Unit = "nanoseconds",
                        Help = "Lowest measurement value in nanoseconds"
                    },
                    Labels = labels,
                    Value = report.AllMeasurements.MinBy(v => v.Nanoseconds).Nanoseconds
                },
                new OpenMetricsMeasurement
                {
                    Header = new OpenMetricsHeader
                    {
                        Name = $"{this.MetricPrefix}_duration_max",
                        Unit = "nanoseconds",
                        Help = "Highest measurement value in nanoseconds"
                    },
                    Labels = labels,
                    Value = report.AllMeasurements.MaxBy(v => v.Nanoseconds).Nanoseconds
                },
                new OpenMetricsMeasurement
                {
                    Header = new OpenMetricsHeader
                    {
                        Name = $"{this.MetricPrefix}_operations",
                        Help = "Amount of actual workload operations in the benchmark"
                    },
                    Labels = labels,
                    Value = report.AllMeasurements.Count(v => v.IterationStage == BenchmarkDotNet.Engines.IterationStage.Actual)
                },
            };

            if(report.BenchmarkCase.Config.HasMemoryDiagnoser())
            {
                metrics.AddRange(new List<OpenMetricsMeasurement>
                {
                    new OpenMetricsMeasurement
                    {
                        Header = new OpenMetricsHeader
                        {
                            Name = $"{this.MetricPrefix}_gen0",
                            Help = "GC Generation 0 collects per 1000 operations"
                        },
                        Labels = labels,
                        Value = report.GcStats.Gen0Collections
                    },
                    new OpenMetricsMeasurement
                    {
                        Header = new OpenMetricsHeader
                        {
                            Name = $"{this.MetricPrefix}_gen1",
                            Help = "GC Generation 1 collects per 1000 operations"
                        },
                        Labels = labels,
                        Value = report.GcStats.Gen1Collections
                    },
                    new OpenMetricsMeasurement
                    {
                        Header = new OpenMetricsHeader
                        {
                            Name = $"{this.MetricPrefix}_gen2",
                            Help = "GC Generation 2 collects per 1000 operations"
                        },
                        Labels = labels,
                        Value = report.GcStats.Gen2Collections
                    },
                    new OpenMetricsMeasurement
                    {
                        Header = new OpenMetricsHeader
                        {
                            Name = $"{this.MetricPrefix}_bytes_allocated",
                            Help = "Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)"
                        },
                        Labels = labels,
                        Value = report.GcStats.GetBytesAllocatedPerOperation(report.BenchmarkCase)
                    }
                });
            }

            foreach(var metric in metrics)
            {
                this.WriteMeasurement(logger, metric);
            }
        }

        /// <summary>
        /// Write a single measurement/metric into a logger.
        /// </summary>
        /// <param name="logger">The ILogger-instance to export to.</param>
        /// <param name="metric">The metric to write.</param>
        private void WriteMeasurement(ILogger logger, OpenMetricsMeasurement metric)
        {
            // Write MetricFamily-header
            // See: https://github.com/OpenObservability/OpenMetrics/blob/main/specification/OpenMetrics.md#metricfamily-1

            logger.WriteLine(string.Format("# TYPE {0} {1}", metric.Header.Name, metric.Header.Type));

            if(!string.IsNullOrEmpty(metric.Header.Unit))
            {
                logger.WriteLine(string.Format("# UNIT {0} {1}", metric.Header.Name, metric.Header.Unit));
            }

            if(!string.IsNullOrEmpty(metric.Header.Help))
            {
                logger.WriteLine(string.Format("# HELP {0} {1}", metric.Header.Name, metric.Header.Help));
            }

            logger.Write(metric.Header.Name);

            if(metric.Labels.Any())
            {
                string[] namedParameters = metric.Labels.Select(v => string.Format("{0}=\"{1}\"", v.Key, v.Value)).ToArray();

                logger.Write("{");
                logger.Write(string.Join(",", namedParameters));
                logger.Write("}");
            }

            logger.Write(" ");

            // TODO: Check whether another value would be more appropriate.
            logger.WriteLine(metric.Value?.ToString() ?? "nan");

            logger.WriteLine();
        }
    }
}