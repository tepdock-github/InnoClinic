using Authorization.Data;
using Authorization.Domain;
using Authorization.Domain.Interfaces;
using Authorization.Domain.Models;
using AuthorizationService;
using AuthorizationService.IdentityServerConfig;
using FluentValidation.AspNetCore;
using IdentityServer4.Models;
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
                .AddInMemoryApiResources(new List<ApiResource>())
                .AddProfileService<CustomProfile>()
                .AddDeveloperSigningCredential();

            builder.Services.AddControllers()
                .AddFluentValidation(s => s.RegisterValidatorsFromAssemblyContaining<Program>());

            builder.Services.AddAutoMapper(typeof(MappingProfile));

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

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();

            #endregion

        }
    }
}