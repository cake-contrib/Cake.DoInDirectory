#tool "nuget:?package=NuGet.CommandLine&version=6.3.1"
#tool "nuget:?package=GitVersion.CommandLine&version=5.11.1"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var nugetApiToken = EnvironmentVariable("nuget_api_token");
var solution = File("Cake.DoInDirectory/Cake.DoInDirectory.sln");

Task("CleanRestore")
    .Does(() => 
{
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
    NuGetRestore(solution);
});

Task("Build")
    .IsDependentOn("CleanRestore")
    .Does(() => 
{
    var version = GitVersion(new GitVersionSettings { UpdateAssemblyInfo = true });
    if (AppVeyor.IsRunningOnAppVeyor) {
        AppVeyor.UpdateBuildVersion(version.InformationalVersion);
    }

    MSBuild(solution, configurator =>
        configurator
            .WithProperty("PackageVersion", version.NuGetVersionV2)
            .UseToolVersion(MSBuildToolVersion.VS2022)
            .SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Minimal));
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() => 
{
    CakeExecuteScript("./test.cake");
});

Task("Publish")
    .WithCriteria(AppVeyor.IsRunningOnAppVeyor)
    .IsDependentOn("Test")
    .Does(() =>
{
    var file = GetFiles("./**/bin/Release/*.nupkg").First();
    AppVeyor.UploadArtifact(file);

    var tagged = AppVeyor.Environment.Repository.Tag.IsTag && 
        !string.IsNullOrWhiteSpace(AppVeyor.Environment.Repository.Tag.Name);

    if (tagged)
    { 
        // Push the package.
        NuGetPush(file, new NuGetPushSettings 
        {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = nugetApiToken
        });
    }
});

Task("Default")
    .IsDependentOn("Publish");

RunTarget(target);
