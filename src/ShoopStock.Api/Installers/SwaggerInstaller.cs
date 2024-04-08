using Microsoft.OpenApi.Models;
using ShoopStock.Core.Constants;
using ShoopStock.Infrastructure.Repositories;
using ShoopStock.Services.Interfaces;
using ShoopStock.Services.Services;

namespace ShoopStock.Api.Installers;

public class SwaggerInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(ForInstaller.ApiVersion, new OpenApiInfo { Title = ForInstaller.ApiTitle, Version = ForInstaller.Version });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = ForInstaller.Bearer
                    }
                },
                Array.Empty<string>()
            }
        });

            c.AddSecurityDefinition(ForInstaller.Bearer, new OpenApiSecurityScheme
            {
                Description = ForInstaller.ApiSecuritySchemeDescription,
                Name = ForInstaller.ApiSecuritySchemeName,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
        });
    }
}
