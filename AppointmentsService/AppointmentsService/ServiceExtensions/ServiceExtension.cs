using AppointmentsService.Consumers.ProfilesConsumers;
using AppointmentsService.Consumers.ServicesConsumers;
using AppointmentsService.Services.Implementation;
using AppointmentsService.Services.Interfaces;
using Appoitments.Data;
using Appoitments.Domain;
using Appoitments.Domain.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AppointmentsService.ServiceExtensions
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
                    Title = "Appoitments service"
                });
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
            b.MigrationsAssembly("AppointmentsService")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void AddAppoitmentService(this IServiceCollection services) =>
            services.AddScoped<IAppoitmentService, AppoitmentService>();

        public static void AddResultService(this IServiceCollection services) =>
            services.AddScoped<IResultService, ResultService>();

        public static void AddScheduleService(this IServiceCollection services) =>
            services.AddScoped<IScheduleService, ScheduleService>();

        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<DoctorProfileManipulationConsumer>();
                x.AddConsumer<PatientProfileConsumer>();
                x.AddConsumer<ServiceManipulationConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("IDoctorProfileManipulation", e =>
                    {
                        e.ConfigureConsumer<DoctorProfileManipulationConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("IPatientProfileManipulation", e =>
                    {
                        e.ConfigureConsumer<PatientProfileConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("IServiceManipulation", e =>
                    {
                        e.ConfigureConsumer<ServiceManipulationConsumer>(context);
                    });
                });
            });
        }
    }
}
