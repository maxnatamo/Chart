using Nuke.Common;

partial class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    protected override void OnBuildInitialized()
    {
        Serilog.Log.Information("🔥 Build process started");
        Serilog.Log.Information("");
        Serilog.Log.Information("Build manifest:");
        Serilog.Log.Information("  Git head: {Head}", GitRepository.Head);
        Serilog.Log.Information("  Git branch: {BranchName}", GitVersion.BranchName);
        Serilog.Log.Information("  Git commit hash: {ShortSha}", GitVersion.ShortSha);
        Serilog.Log.Information("  Git semantic version: {SemVer}", GitVersion.SemVer);

        base.OnBuildInitialized();
    }
}
