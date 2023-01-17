﻿using Microsoft.EntityFrameworkCore;
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
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
            b.MigrationsAssembly("ServicesService")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServicesManager(this IServiceCollection services) =>
            services.AddScoped<IServicesManager, ServicesManager>();
    }
}
