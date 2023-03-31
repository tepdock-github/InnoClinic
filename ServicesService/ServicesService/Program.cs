using CustomExceptionMiddleware;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ServicesService.Filters;
using ServicesService.ServiceExtensions;

namespace ServicesService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            var connectionString = builder.Configuration.GetConnectionString("sqlConnection");
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.ConfigureServiceServices();
            builder.Services.ConfigureSpecializationServices();
            builder.Services.ConfigureCategoryServices();

            builder.Services.AddControllers();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://auth-api:80";
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

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

            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
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

            #endregion

            var app = builder.Build();

            #region Middlewares/pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               // app.UseHsts();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("authMicroservice");
                c.OAuthClientSecret("innoClinicSecret");
                c.OAuthUsePkce();
            });

            app.Run();
            #endregion
        }
    }
}