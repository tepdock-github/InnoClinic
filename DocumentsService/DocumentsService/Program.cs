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
            #endregion

            var app = builder.Build();

            #region Middlewares/pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseStaticFiles();
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