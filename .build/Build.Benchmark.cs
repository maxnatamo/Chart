using Nuke.Common;
using Nuke.Common.Tools.DotNet;

partial class Build
{
    Target Benchmark => _ => _
        .Before(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetRun(c => c
                .SetProjectFile(BenchmarkSourceDirectory)
                .SetConfiguration(Configuration.Release));
        });
}
