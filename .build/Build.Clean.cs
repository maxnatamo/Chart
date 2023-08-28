using Nuke.Common;
using Nuke.Common.Tools.DotNet;

partial class Build
{
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            DotNetTasks.DotNetClean(c => c.SetProject(SolutionFilePath));
        });
}
