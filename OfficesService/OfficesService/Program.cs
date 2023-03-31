using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OfficesService.Data.Repositories;
using OfficesService.Domain;
using OfficesService.Domain.Interfaces;
using OfficesService.Filters;
using OfficesService.ServiceExtensions;
using OfficesService.Services.Implementation;

namespace OfficesService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<OfficeDatabaseSettings>(
                builder.Configuration.GetSection(nameof(OfficeDatabaseSettings)));

            builder.Services.AddSingleton<IOfficeDatabaseSettings>(provider => 
                provider.GetRequiredService<IOptions<OfficeDatabaseSettings>>().Value);
            builder.Services.ConfigureRepository();
            builder.Services.AddScoped<ValidateModelFilter>();
            builder.Services.AddSingleton<ImageService>();
            builder.Services.ConfigureOfficeService();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://auth-api";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "gatewayAPI";
                });

            builder.Services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.ConfigureSwagger();
            builder.Services.AddSwaggerGen(s =>
            {
                s.IncludeXmlComments("swagger.xml");
            });


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
            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}