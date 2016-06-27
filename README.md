# Cake.DoInDirectory
An extension to Cake to do this

```cs
    #addin nuget:?package=Cake.DoInDirectory
    
    DoInDirectory("Some/Sub/Directory", () =>
        Npm.RunScript("test")
    );
```

[![Build status](https://ci.appveyor.com/api/projects/status/1vdj6p5b4d5h6b7v?svg=true)](https://ci.appveyor.com/project/pitermarx/cake-doindirectory)
[![NuGet](https://img.shields.io/nuget/v/Cake.DoInDirectory.svg?maxAge=2592000)](https://www.nuget.org/packages/Cake.DoInDirectory/)
