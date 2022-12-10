using Authorization.Data;
using Authorization.Domain;
using Authorization.Domain.Interfaces;
using Authorization.Domain.Models;
using AuthorizationService;
using AuthorizationService.IdentityServerConfig;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddIdentity<Account, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<Account>()
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(new List<ApiResource>())
                .AddProfileService<CustomProfile>()
                .AddDeveloperSigningCredential();

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

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

            app.Run();

            #endregion

        }
    }
}