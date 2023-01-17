using Microsoft.AspNetCore.Mvc;
using ServicesService.Domain.Entities;
using ServicesService.Filters;
using ServicesService.ServiceExtensions;

namespace ServicesService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Services
            var connectionString = builder.Configuration.GetConnectionString("sqlConnection");
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.ConfigureServicesManager();
            builder.Services.AddControllers();

            builder.Services.AddAuthorization();

            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();

            builder.Services.AddScoped<ValidateModelFilter>();

            builder.Services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.ConfigureSwagger();
            builder.Services.AddSwaggerGen(s =>
            {
                s.IncludeXmlComments("swagger.xml");
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}