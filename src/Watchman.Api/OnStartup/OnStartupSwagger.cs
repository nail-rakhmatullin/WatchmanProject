﻿using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NJsonSchema.Generation;
using NSwag;
using NSwag.Generation;
using NSwag.Generation.Processors.Security;

namespace Watchman.Api.OnStartup {

  public static class OnStartupSwagger {

    /// <summary>
    ///     Add Swagger
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service description</param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services) {
      services.AddOpenApiDocument(document => {
        document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme {
          Type = OpenApiSecuritySchemeType.ApiKey,
          Name = "Authorization",
          In = OpenApiSecurityApiKeyLocation.Header,
          Description = "Type into the textbox: Bearer {your JWT token}."
        });

        document.OperationProcessors.Add(
            new AspNetCoreOperationSecurityScopeProcessor("JWT"));

        document.Title = "Watchman.Api";
        document.Version = "v1.0.0-dev";
        document.Description = "Watchman.Api test description";
        document.SchemaGenerator = new OpenApiSchemaGenerator(new JsonSchemaGeneratorSettings {
          GenerateExamples = true,
          ReflectionService = new DefaultReflectionService(),
          SchemaType = SchemaType.OpenApi3,
          AllowReferencesWithProperties = true,
          GenerateKnownTypes = true
        });
      });

      return services;
    }
  }
}