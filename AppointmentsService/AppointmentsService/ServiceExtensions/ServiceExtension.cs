using AppointmentsService.Services.Implementation;
using AppointmentsService.Services.Interfaces;
using Appoitments.Data;
using Appoitments.Domain;
using Appoitments.Domain.Interfaces;
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
    }
}
