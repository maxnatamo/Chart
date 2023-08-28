using Nuke.Common;
using Nuke.Common.CI.GitHubActions;

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    OnPushBranches = new[]
    {
        "main"
    },
    OnPushIncludePaths = new[]
    {
        "src/**"
    },
    InvokedTargets = new[]
    {
        nameof(Pack)
    }
)]
partial class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild
        ? Configuration.Debug
        : Configuration.Release;

    protected override void OnBuildInitialized()
    {
        Serilog.Log.Information("🔥 Build process started");

        base.OnBuildInitialized();
    }
}
