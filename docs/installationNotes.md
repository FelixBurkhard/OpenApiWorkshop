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

## Documentation

use [Attributes](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/include-metadata?view=aspnetcore-9.0&tabs=controllers) to add documentation of your API directly into your code
Hint to use reflection to write tests that fail if a endpoint is undocumented.
probably could use custom analyzer for this as well.

## Open Points

- [Spectral](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/using-openapi-documents?view=aspnetcore-9.0#lint-generated-openapi-documents-with-spectral) for linting?
- Wirklich als webapp hosten oder besser lokale doku mit [Scalar](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/using-openapi-documents?view=aspnetcore-9.0#use-scalar-for-interactive-api-documentation)
