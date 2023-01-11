using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AuthorizationService
{
    public class AspIdentityConfig : IConfigureOptions<IdentityOptions>
    {
        private readonly IConfiguration _configuration;

        public AspIdentityConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(IdentityOptions options)
        {
            var passwordSettings = _configuration.GetSection("PasswordConfiguration");
            options.Password.RequiredLength = int.Parse(passwordSettings.GetSection("MinRequiredLength").Value);
            options.Password.RequireNonAlphanumeric = bool.Parse(passwordSettings.GetSection("PasswordRequireNonAlphanumeric").Value);
            options.Password.RequireUppercase = bool.Parse(passwordSettings.GetSection("PasswordRequireUppercase").Value);
            options.Password.RequireLowercase = bool.Parse(passwordSettings.GetSection("PasswordRequireLowercase").Value);
        }
    }
}
