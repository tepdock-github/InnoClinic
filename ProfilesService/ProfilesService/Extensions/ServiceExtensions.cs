using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProfileService.Data;
using ProfilesService.Domain;
using ProfilesService.Domain.Interfaces;
using ProfilesService.Services.Implimentation;
using ProfilesService.Services.Interfaces;

namespace ProfilesService.Extensions
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
                    Title = "Service's service"
                });
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
            b.MigrationsAssembly("ProfilesService")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureDoctorService(this IServiceCollection services) =>
            services.AddScoped<IDoctorProfileService, DoctorProfileService>();

        public static void ConfigurePatientService(this IServiceCollection services) =>
            services.AddScoped<IPatientProfileService, PatientProfileService>();

        public static void ConfigureReceptionistService(this IServiceCollection services) =>
            services.AddScoped<IReceptionistProfileService, ReceptionistProfileeService>();
    }
}
