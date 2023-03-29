using Authorization.Domain.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthorizationService.IdentityServerConfig
{
    public class CustomProfile : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<Account> _claimsFactor;
        private readonly UserManager<Account> _userManager;

        public CustomProfile(IUserClaimsPrincipalFactory<Account> claimsFactor, 
            UserManager<Account> userManager)
        {
            _claimsFactor = claimsFactor;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject;
            var user = await _userManager.GetUserAsync(subject);

            string roleName = "";
            string email = "";
            string guid = "";
            if (user != null) 
            {
                var role = await _userManager.GetRolesAsync(user);
                roleName = role.FirstOrDefault().ToString();
                email = user.Email;
                guid = user.Id;
            }

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Id, guid),
                new Claim(JwtClaimTypes.Email, email),
                new Claim(JwtClaimTypes.Role, roleName)
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = user != null;
        }

    }
}
