using Microsoft.AspNetCore.Mvc;
using CustomExceptionMiddleware;
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
            var connectionString = builder.Configuration.GetConnectionString("sqlConnection");
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();

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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:7111", "http://gateway:80")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
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
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("CorsPolicy");

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