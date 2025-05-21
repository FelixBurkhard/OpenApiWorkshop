# Notes for Workshop

## Generating OpenAPI Spec

There are two relevant packages.
`Microsoft.AspNetCore.OpenApi` builds and serves the openapi.json during runtime.
`Microsoft.Extensions.ApiDescription.Server` creates the openapi.json during build time so it can be used in the pipelines.
The later one puts the file in the `obj` folder and its named `{ProjectName}.json`.

[source](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-9.0&tabs=visual-studio%2Cvisual-studio-code)

Its available from .net8.
For older version you can use swashbuckle.

## Swagger UI for debugging

[source](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/using-openapi-documents?view=aspnetcore-9.0)

## Auto Client Generator

### NSwag

creates client that can easily be packaged.
`NSwag.CodeGeneration.CSharp` package.
[source](https://github.com/RicoSuter/NSwag/wiki/CSharpClientGenerator)

```xml
  <Target Name="CopyOpenApiJson" AfterTargets="Build">
    <PropertyGroup>
      <ApiProjectDir>..\ProjectManagerSimulatorApi</ApiProjectDir>
      <OpenApiSource>$(ApiProjectDir)\obj\ProjectManagerSimulatorApi.json</OpenApiSource>
      <OpenApiDest>$(OutDir)\ProjectManagerSimulatorApi.json</OpenApiDest>
    </PropertyGroup>
    <Copy SourceFiles="$(OpenApiSource)" DestinationFiles="$(OpenApiDest)" SkipUnchangedFiles="true" />
  </Target>
```

## Publish nuget feed

See github action file.
Add new package source in VS.
if asked for credentials use your github username and a generated github PAT with package read permissions as password.
If you messed it up go to Control Panel\User Accounts\Credential Manager and change it or just delte it to get another prompt.
Change namespace and class name via commandline of nswag.

## Documentation

use [Attributes](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/include-metadata?view=aspnetcore-9.0&tabs=controllers) to add documentation of your API directly into your code
Hint to use reflection to write tests that fail if a endpoint is undocumented.
probably could use custom analyzer for this as well.

### Docosaurus
you need to run this in WSL2.
navigate to windows paths using `/mnt/<drive-letter>`
`npx create-docusaurus@3.7.0 docs --package-manager yarn`
[source](https://github.com/PaloAltoNetworks/docusaurus-openapi-docs?tab=readme-ov-file#bootstrapping-from-template-new-docusaurus-site)
Github Pages are public.
If you need private pages you need to have github enterprise.

## Open Points

- [Spectral](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/using-openapi-documents?view=aspnetcore-9.0#lint-generated-openapi-documents-with-spectral) for linting?
- Wirklich als webapp hosten oder besser lokale doku mit [Scalar](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/using-openapi-documents?view=aspnetcore-9.0#use-scalar-for-interactive-api-documentation)

github [supports spoilers](https://github.com/dear-github/dear-github/issues/166#issuecomment-2615537189):

```md
<details>
  <summary>Click to expand</summary>
  whatever
</details>
```
