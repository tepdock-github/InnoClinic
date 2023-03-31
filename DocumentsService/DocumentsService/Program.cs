using CustomExceptionMiddleware;
using DocumentService.Data;
using DocumentService.Domain.Interfaces;
using DocumentsService.Services;
using DocumentsService.ServicesExtensions;

namespace DocumentsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.ConfigureSwagger();
            builder.Services.AddSwaggerGen(s =>
            {
                s.IncludeXmlComments("swagger.xml");
            });

            builder.Services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();
            builder.Services.AddTransient<IBlobService, BlobService>();

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

            builder.Services.ConfigureMassTransit();
            #endregion

            var app = builder.Build();

            #region Middlewares/pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
            #endregion
        }
    }
}