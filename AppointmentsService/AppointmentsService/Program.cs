using AppointmentsService.Filters;
using AppointmentsService.ServiceExtensions;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentService 
{
    public class Program
    {
        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            var connectionString = builder.Configuration.GetConnectionString("sqlConnection");

            builder.Services.AddAuthorization();

            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();
            builder.Services.AddAppoitmentService();
            builder.Services.AddResultService();

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

            #region Middlewares/pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
            #endregion

        }
    }
}