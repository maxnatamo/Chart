using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.ReportGenerator;

using System.Collections.Generic;

partial class Build
{
    IEnumerable<Project> TestProjects => SolutionModelTasks
        .ReadSolution(SolutionFilePath)
        .GetAllProjects("*.Tests");

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(c => c
                .SetNoBuild(false)
                .SetNoRestore(true)
                .SetConfiguration(Configuration.Debug)
                .EnableCollectCoverage()
                .SetCoverletOutput(CoverletOutputFormat.opencover)
                .SetProcessArgumentConfigurator(a => a.Add("--collect:\"XPlat Code Coverage\""))
                .CombineWith(TestProjects, (_, project) => _
                    .SetProjectFile(project)
                    .SetCoverletOutputFormat(CoverletOutputFormat.opencover)
                    .SetCoverletOutput(TestCoverageDirectory / $"{project.Name}.xml")));
        });

    Target Report => _ => _
        .DependsOn(Test)
        .Consumes(Test)
        .Executes(() =>
        {
            ReportGeneratorTasks.ReportGenerator(c => c
                .SetTargetDirectory(ReportDirectory)
                .SetReportTypes(ReportTypes.Cobertura, ReportTypes.Html)
                .SetReports(TestCoverageDirectory / "*.xml")
                .SetAssemblyFilters("-*Tests"));
        });
}