# Packaging NProxy

Create the NuGet package.

```
> nuget pack Source\Main\NProxy.Core\NProxy.Core.csproj -Properties Configuration=Release -Version x.y.z
```

Push the NuGet package to the server.

```
> nuget push NProxy.Core.x.y.z.nupkg
```
