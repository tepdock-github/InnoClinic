using IdentityServer4;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServicesService.Data;
using ServicesService.Domain;
using ServicesService.Domain.Interfaces;
using ServicesService.Services;
using ServicesService.ServicesInterfaces;

namespace ServicesService.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Service's service"
                });

                s.AddSecurityDefinition("oath2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                       Password = new OpenApiOAuthFlow
                       {
                           AuthorizationUrl = new Uri("http://localhost:5010/connect/authorize"),
                           TokenUrl = new Uri("http://localhost:5010/connect/token"),
                           Scopes = new Dictionary<string, string>
                           {
                               {"gatewayAPI", "Gateway API"},
                               { IdentityServerConstants.StandardScopes.OpenId, "OpenID Connect" },
                               { IdentityServerConstants.StandardScopes.Profile, "User profile" },
                               { IdentityServerConstants.StandardScopes.Email, "User email" },
                               { IdentityServerConstants.StandardScopes.OfflineAccess, "Offline access" }
                           }
                       }
                    }
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oath2"
                            }
                        },
                        new [] { "gatewayAPI", IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OfflineAccess }
                    }
                });

            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
            b.MigrationsAssembly("ServicesService")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureCategoryServices(this IServiceCollection services) =>
            services.AddScoped<ICategoryServices, CategoryServices>();

        public static void ConfigureServiceServices(this IServiceCollection services) =>
            services.AddScoped<IServiceServices, ServiceServices>();

        public static void ConfigureSpecializationServices(this IServiceCollection services) =>
            services.AddScoped<ISpecializationServices, SpecializationServices>();
    }
}
