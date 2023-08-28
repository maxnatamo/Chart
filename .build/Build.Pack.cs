using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.NuGet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build : NukeBuild
{
    Target Pack => _ => _
        .DependsOn(Compile, Format, Test)
        .Produces(NuGetArtifactsDirectory / "*.nupkg")
        .Produces(NuGetArtifactsDirectory / "*.snupkg")
        .Requires(() => Configuration.IsRelease)
        .Executes(() =>
            DotNetPack(s => s
                .SetProject(SolutionFilePath)
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetIncludeSymbols(false)
                .SetIncludeSource(false)
                .SetDescription("GraphQL execution engine for .NET Core")
                .SetAuthors("Max T. Kristiansen")
                .SetCopyright("Copyright (c) Max T. Kristiansen 2023")
                .SetPackageTags("graphql c# core library")
                .SetPackageProjectUrl("https://github.com/maxnatamo/chart")
                .SetNoDependencies(false)
                .SetOutputDirectory(NuGetArtifactsDirectory)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetVersion(GitVersion.SemVer)));
}