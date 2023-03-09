using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build : NukeBuild
{
    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(c => c.SetProjectFile(MainSolutionFile));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(c => c
                .SetProjectFile(MainSolutionFile)
                .SetNoRestore(InvokedTargets.Contains(Restore))
                .SetConfiguration(this.Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion));
        });
}
