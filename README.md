# Cake.DoInDirectory
An extension to Cake to do this

```cs
    InDirectory("Some/Sub/Directory", () =>
        Npm.RunScript("test")
    );
```
