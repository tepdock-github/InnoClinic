using DocumentsService.Consumers;
using MassTransit;
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

        public static void ConfigureMassTransit(this IServiceCollection services) 
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ResultManipulationConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("IResultManipulation", e =>
                    {
                        e.ConfigureConsumer<ResultManipulationConsumer>(context);
                    });
                });
            });
        }
    }
}
