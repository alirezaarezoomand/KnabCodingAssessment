using Knab.CodingAssessment.Exchange.Endpoint.WebApi.Filters;
using Microsoft.OpenApi.Models;

namespace Knab.CodingAssessment.Exchange.Endpoint.WebApi.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, ApiConfiguration apiConfiguration)
        {
            services.AddSwaggerGen(options =>
            {
                foreach (var apiVersion in apiConfiguration.ApiVersions)
                {
                    options.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiConfiguration.ApiName, Version = apiVersion });
                }

                options.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
                {
                    Description = "Api key needed to access the endpoints. X-Api-Key: My_API_Key",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Authorization"
                            },
                         },
                         new string[] {}
                     }
                });

                options.OperationFilter<SwaggerRequestHeaderFilter>();
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerUi(this IApplicationBuilder app, ApiConfiguration apiConfiguration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var apiVersion in apiConfiguration.ApiVersions)
                {
                    c.SwaggerEndpoint($"{apiConfiguration.ApiBaseUrl}/swagger/{apiVersion}/swagger.json", $"{apiConfiguration.ApiName} {apiVersion}");
                    c.RoutePrefix = string.Empty;
                }
            });
            return app;
        }
    }

}
