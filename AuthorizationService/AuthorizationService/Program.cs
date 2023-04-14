using Authorization.Data;
using Authorization.Domain;
using Authorization.Domain.Interfaces;
using Authorization.Domain.Models;
using AuthorizationService;
using AuthorizationService.IdentityServerConfig;
using CustomExceptionMiddleware;
using EmailService;
using EmailService.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthorizationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            var connectionString = builder.Configuration.GetConnectionString("sqlConnection");

            builder.Services.AddDbContext<RepositoryContext>(
                options =>
                options.UseSqlServer(connectionString,
                x => x.MigrationsAssembly("AuthorizationService")));
            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

            builder.Services.AddSingleton<IConfigureOptions<IdentityOptions>, AspIdentityConfig>();

            builder.Services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<Account>()
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddProfileService<CustomProfile>()
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
                .AddDeveloperSigningCredential();

            var emailConfig = builder.Configuration
                .GetSection("EmailConfig")
                .Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<IEmailService, EmailService.Services.EmailService>();

            builder.Services.AddControllers()
                .AddFluentValidation(s => s.RegisterValidatorsFromAssemblyContaining<Program>());

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://localhost:7111")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
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
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("CorsPolicy");
            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();

            #endregion

        }
    }
}