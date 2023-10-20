using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API.Setup
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerGenConfig(this IServiceCollection services)
        {
            var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);

            services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization Token",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }});

                config.CustomSchemaIds(x => x.FullName);
                config.IncludeXmlComments(xmlPath);
            });
        }
    }
}
