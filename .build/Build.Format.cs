using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build : NukeBuild
{
    Target Format => _ => _
        .Executes(() =>
        {
            DotNetFormat(c => c
                .SetProject(MainSolutionFile)
                .SetSeverity(DotNetFormatSeverity.error)
                .SetVerifyNoChanges(true));
        });
}
