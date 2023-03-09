using Nuke.Common;

partial class Build : NukeBuild
{
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });
}
