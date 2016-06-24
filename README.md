# Cake.DoInDirectory
An extension to Cake to do this

```cs
    #addin nuget:?package=Cake.DoInDirectory&prerelease
    
    DoInDirectory("Some/Sub/Directory", () =>
        Npm.RunScript("test")
    );
```
