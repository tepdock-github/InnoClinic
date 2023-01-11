using IdentityServer4;
using IdentityServer4.Models;

namespace AuthorizationService.IdentityServerConfig
{
    public static class IdentityServerConfiguration
    {
        public static List<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId= "authMicroservice",
                    ClientSecrets = new [] {new Secret("innoClinicSecret".Sha512())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId}
                }
            };
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
                  new IdentityResources.OpenId(),
                  new IdentityResources.Profile(),
                  new IdentityResources.Email()
          };

    }
}
