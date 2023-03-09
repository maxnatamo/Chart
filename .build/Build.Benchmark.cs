using System.IO;
using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build : NukeBuild
{
    Target Benchmark => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            /**
             * This method is a little janky, as the official BenchmarkDotNet tool
             * is deprecated and has not been updated in Nuke.
             */

            foreach(var projectFile in BenchmarkProjectFiles)
            {
                DotNetRun(s => s
                    .SetProjectFile(projectFile)
                    .SetConfiguration(Configuration.Release)
                    .EnableNoRestore()
                    .EnableNoBuild());
            }
        });

    Target Metrics => _ => _
        .DependsOn(Benchmark)
        .OnlyWhenDynamic(() => IsServerBuild)
        .Executes(() =>
        {
            /**
             * This part of the method concatenates all .metrics-files from the benchmarks
             * into a single metrics.txt, which is consumed by GitLab CI.
             */

            var benchmarkArtifactsDirectory = RootDirectory / "BenchmarkDotNet.Artifacts" / "results";

            if(File.Exists(MetricsFilePath))
            {
                File.Delete(MetricsFilePath);
            }

            foreach(var resultFile in Directory.EnumerateFiles(benchmarkArtifactsDirectory))
            {
                if(!resultFile.EndsWith(".metrics"))
                {
                    continue;
                }

                string resultContent = File.ReadAllText(resultFile);

                File.AppendAllText(MetricsFilePath, resultContent);
            }

            File.AppendAllText(MetricsFilePath, "# EOF");
        });
}
