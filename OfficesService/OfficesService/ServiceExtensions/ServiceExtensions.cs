using Microsoft.OpenApi.Models;

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
    }
}
