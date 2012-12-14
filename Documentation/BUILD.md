# Building NProxy

Install the dependent NuGet packages.

```
> cd Lib\
> nuget install packages.config
```

Build the project with your preferred tool.

# Packaging NProxy

Create the NuGet package.

```
> nuget pack Source\Main\NProxy.Core\NProxy.Core.csproj -Properties Configuration=Release
```

Push the NuGet package to the server.

```
> nuget push NProxy.Core.x.x.x.x.nupkg
```
