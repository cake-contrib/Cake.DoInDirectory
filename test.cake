#reference "Cake.DoInDirectory/bin/Release/net8.0/Cake.DoInDirectory.dll"

Setup((ctx) => 
{
    CreateDirectory("tests");
    CleanDirectory("tests");
    CopyDirectory("Cake.DoInDirectory/bin", "tests");
});

Task("Test")
    .Does(() => 
{
    if (!DirectoryExists("tests")) 
    {
        throw new Exception("[Before] Expected tests dir to exist");
    }
    
    if (DirectoryExists("Release"))
    {
        throw new Exception("[Before] Expected Release dir not to exist");
    }

    DoInDirectory("tests", () => 
    {
        if (!DirectoryExists("Release"))
        {
            throw new Exception("Expected Release dir to exist");
        }
    });
    
    if (!DirectoryExists("tests")) 
    {
        throw new Exception("[After] Expected tests dir to exist");
    }
    
    if (DirectoryExists("Release"))
    {
        throw new Exception("[After] Expected Release dir not to exist");
    }

});

RunTarget("Test");
