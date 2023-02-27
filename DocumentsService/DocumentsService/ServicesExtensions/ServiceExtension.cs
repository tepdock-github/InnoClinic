using Microsoft.OpenApi.Models;

namespace DocumentsService.ServicesExtensions
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
            });
        }
    }
}
