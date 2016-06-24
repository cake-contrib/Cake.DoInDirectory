# Cake.DoInDirectory
An extension to Cake to do this

```cs
    DoInDirectory("Some/Sub/Directory", () =>
        Npm.RunScript("test")
    );
```
