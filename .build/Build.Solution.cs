using System.Collections.Generic;
using System.IO;
using System.Linq;

using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;

partial class Build
{
    private static readonly AbsolutePath[] Projects =
    {
        RootDirectory / "src" / "Benchmarks",
        RootDirectory / "src" / "Core",
        RootDirectory / "src" / "Language",
        RootDirectory / "src" / "Utilities",
        RootDirectory / "src" / "Testing",
    };

    private static IEnumerable<string> GetAllProjects()
    {
        foreach(AbsolutePath projectDirectory in Projects)
        {
            foreach(string projectFile in Directory.EnumerateFiles(projectDirectory, "*.csproj", SearchOption.AllDirectories))
            {
                yield return projectFile;
            }
        }
    }

    private void GenerateSolutionFile()
    {
        if(File.Exists(SolutionFilePath))
        {
            File.Delete(SolutionFilePath);
        }

        string workingDirectory = Path.GetDirectoryName(SolutionFilePath);
        string projects = string.Join(" ", Build.GetAllProjects().Select(p => $"\"{p}\""));

        DotNetTasks.DotNet($"new sln -n {Path.GetFileNameWithoutExtension(SolutionFilePath)}", workingDirectory);
        DotNetTasks.DotNet($"sln {SolutionFilePath} add {projects}", workingDirectory);
    }
}
