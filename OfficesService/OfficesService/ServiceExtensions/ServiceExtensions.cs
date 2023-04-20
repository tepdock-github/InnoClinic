using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OfficesService.Data;
using OfficesService.Data.Repositories;
using OfficesService.Domain;
using OfficesService.Domain.Interfaces;
using OfficesService.Services.Implementation;
using OfficesService.Services.Interfaces;

namespace OfficesService.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Office service"
                });
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
            b.MigrationsAssembly("OfficesService")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureOfficeService(this IServiceCollection services) =>
            services.AddScoped<IOfficeService, OfficeService>();
    }
}
