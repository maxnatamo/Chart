using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.IO.PathConstruction;

partial class Build
{
    Target Restore => _ => _
        .Executes(() =>
        {
            GenerateSolutionFile();
            DotNetTasks.DotNetRestore(c => c.SetProjectFile(SolutionFilePath));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            if(!InvokedTargets.Contains(Restore))
            {
                GenerateSolutionFile();
            }

            DotNetTasks.DotNetBuild(c => c
                .SetProjectFile(SolutionFilePath)
                .SetNoRestore(InvokedTargets.Contains(Restore))
                .SetConfiguration(Configuration));
        });
}
