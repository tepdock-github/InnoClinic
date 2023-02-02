using Microsoft.OpenApi.Models;
using OfficesService.Data.Repositories;
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

        public static void ConfigureRepository(this IServiceCollection services) =>
            services.AddScoped<IOfficeRepository, OfficeRepository>();
        public static void ConfigureOfficeService(this IServiceCollection services) =>
            services.AddScoped<IOfficeService, OfficeService>();
    }
}
