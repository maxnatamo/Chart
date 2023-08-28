using Nuke.Common.IO;
using Nuke.Common.Tools.GitVersion;

partial class Build
{
    private AbsolutePath SourceDirectory => RootDirectory / "src";
    private AbsolutePath SolutionFilePath => SourceDirectory / "Chart.sln";

    /// <summary>
    /// Path containing the benchmarking executable.
    /// </summary>
    private AbsolutePath BenchmarkSourceDirectory => SourceDirectory / "src" / "Benchmarks" / "src";

    /// <summary>
    /// Path to store test coverage files.
    /// </summary>
    private AbsolutePath TestCoverageDirectory => RootDirectory / "coverage";

    /// <summary>
    /// Path to store testing reports.
    /// </summary>
    private AbsolutePath ReportDirectory => RootDirectory / "report";

    /// <summary>
    /// Path to store local artifacts.
    /// </summary>
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "artifacts";

    /// <summary>
    /// Path to store local NuGet artifacts.
    /// </summary>
    readonly AbsolutePath NuGetArtifactsDirectory = RootDirectory / "artifacts" / "nuget";

    [GitVersion(Framework = "net7.0", NoFetch = true)]
    readonly GitVersion GitVersion;
}