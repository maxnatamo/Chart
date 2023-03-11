using Nuke.Common.IO;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitVersion;

partial class Build
{
    /// <summary>
    /// Path to the main solution file of the project.
    /// </summary>
    AbsolutePath MainSolutionFile = RootDirectory / "Chart.sln";

    /// <summary>
    /// Paths to all benchmarks in the solution.
    /// </summary>
    AbsolutePath[] BenchmarkProjectFiles =
    {
        RootDirectory / "Chart.Core.Parsers.Benchmarks" / "Chart.Core.Parsers.Benchmarks.csproj"
    };

    /// <summary>
    /// Path to the metrics.txt report, defined by GitLab CI.
    /// </summary>
    /// <seealso href="https://docs.gitlab.com/ee/ci/testing/metrics_reports.html">GitLab documentation.</seealso>
    AbsolutePath MetricsFilePath = RootDirectory / "metrics.txt";

    /// <summary>
    /// Path to coverage reports.
    /// </summary>
    AbsolutePath TestCoverageDirectory = RootDirectory / "coverage";

    [GitRepository]
    readonly GitRepository GitRepository;
}