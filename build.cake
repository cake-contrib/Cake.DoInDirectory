#tool "nuget:?package=GitVersion.CommandLine&version=3.6.2"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var nugetApiToken = EnvironmentVariable("nuget_api_token");
var solution = File("Cake.DoInDirectory/Cake.DoInDirectory.sln");

var version = GitVersion(new GitVersionSettings { UpdateAssemblyInfo = true });
if (AppVeyor.IsRunningOnAppVeyor) {
    AppVeyor.UpdateBuildVersion(version.InformationalVersion);
}

Task("Clean")
    .Does(() => 
{
    CreateDirectory("./nuget");
    CleanDirectory("./nuget");
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() => 
{
    NuGetRestore(solution);
    MSBuild(solution, configurator =>
        configurator
            .SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Minimal));
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() => 
{
    CakeExecuteScript("./test.cake");
});

Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
{
    var nuGetPackSettings   = new NuGetPackSettings 
    {
        Id           = "Cake.DoInDirectory",
        Version      = version.NuGetVersionV2,
        Authors      = new[] {"pitermarx"},
        Description  = "An extension to Cake Build. Changes the current directory.",
        ProjectUrl   = new Uri("https://github.com/pitermarx/Cake.DoInDirectory"),
        Tags         = new [] {"cake","io", "directory"},
        IconUrl      = new Uri("https://cdn.rawgit.com/cake-contrib/graphics/a5cf0f881c390650144b2243ae551d5b9f836196/png/cake-contrib-medium.png"),
        Files        = new [] { 
            new NuSpecContent { Source = "Cake.DoInDirectory/bin/Release/Cake.DoInDirectory.dll", Target = "lib/net45" },
            new NuSpecContent { Source = "Cake.DoInDirectory/bin/Release/Cake.DoInDirectory.XML", Target = "lib/net45" },
            new NuSpecContent { Source = "Cake.DoInDirectory/bin/Release/Cake.DoInDirectory.pdb", Target = "lib/net45" }
        },
        Dependencies = new [] {
            new NuSpecDependency { Id = "Cake.Core", Version = "0.18.0" }
        },
        BasePath        = "./",
        OutputDirectory = "./nuget"
    };

    NuGetPack(nuGetPackSettings);
});

Task("Publish")
    .WithCriteria(AppVeyor.IsRunningOnAppVeyor)
    .IsDependentOn("Pack")
    .Does(() =>
{
    var file = GetFiles("nuget/*.nupkg").First();
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
