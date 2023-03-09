using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tools.Coverlet;

partial class Build : NukeBuild
{
    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(c => c
                .SetProjectFile(MainSolutionFile)
                .SetCoverletOutput(TestCoverageDirectory / "cobertura-coverage.xml")
                .SetCoverletOutputFormat(CoverletOutputFormat.cobertura)
                .SetCollectCoverage(true)
                .EnableNoBuild()
                .EnableNoRestore());
        });
}
