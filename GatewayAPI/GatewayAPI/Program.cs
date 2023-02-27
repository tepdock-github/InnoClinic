using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewayAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services

            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot(builder.Configuration);

            #endregion

            var app = builder.Build();

            #region Middlewares/pipeline

            app.UseOcelot();

            app.Run();

            #endregion
        }
    }
}