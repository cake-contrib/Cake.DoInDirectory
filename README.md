| README.md |
|:---|

<div align="center">

![Cake.DoInDirectory](asset/cake-doindirectory-logo.png)

</div>

<h1 align="center">Cake.DoInDirectory</h1>
<div align="center">

Addin for the [Cake](https://cakebuild.net) build automation system that enables you easily execute code in a different working directory.

[![NuGet Version](https://img.shields.io/nuget/v/Cake.DoInDirectory.svg?color=blue&style=flat-square)](https://www.nuget.org/packages/Cake.DoInDirectory/) [![Stack Overflow Cake Build](https://img.shields.io/badge/stack%20overflow-cakebuild-orange.svg?style=flat-square)](http://stackoverflow.com/questions/tagged/cakebuild)

</div>

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Getting started :rocket:

Simply add `Cake.DoInDirectory` in your build script by using the [`addin`](http://cakebuild.net/docs/writing-builds/preprocessor-directives#add-in-directive) directive:

```csharp
#addin "nuget:?package=Cake.DoInDirectory&version=x.y.z"
```

_Make sure the `&version=` attribute references the [latest version of Cake.DoInDirectory](https://www.nuget.org/packages/Cake.DoInDirectory/) compatible with the Cake runner that you are using. Check the [compatibility table](#compatibility) to see which version of Cake.DoInDirectory to choose_.

The `DoInDirectory` method temporarily switches to a new working directory, executes the code block you want in that directory, and switches back to the original working directory at the end.

```csharp
#addin "nuget:?package=Cake.DoInDirectory&version=x.y.z"

DoInDirectory("Some/Sub/Directory", () =>
    Npm.RunScript("test")
);
```

## Compatibility

Cake.DoInDirectory is compatible with all [Cake runners](https://cakebuild.net/docs/running-builds/runners/), and below you can find which version of Cake.DoInDirectory you should use based on the version of the Cake runner you're using.

| Cake runner     | Cake.DoInDirectory | Cake addin directive                                       |
|:---------------:|:------------------:| ---------------------------------------------------------- |
| 1.0.0 or higher | 4.0.0 or higher    | `#addin "nuget:?package=Cake.DoInDirectory&version=4.0.2"` |
| 0.33.0 - 0.38.5 | 3.3.0              | `#addin "nuget:?package=Cake.DoInDirectory&version=3.3.0"` |
| < 0.33.0        | _N/A_              | _(not supported)_                                          |

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## Release History

Click on the [Releases](https://github.com/cake-contrib/Cake.DoInDirectory/releases) tab on GitHub.

---

_Copyright &copy; 2016-2021 Cake Contributors - Provided under the [MIT License](LICENSE)._
