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
    /// Path to coverage reports.
    /// </summary>
    AbsolutePath TestCoverageDirectory = RootDirectory / "coverage";

    [GitRepository]
    readonly GitRepository GitRepository;

    [GitVersion(Framework = "net6.0", NoFetch = true)]
    readonly GitVersion GitVersion;
}