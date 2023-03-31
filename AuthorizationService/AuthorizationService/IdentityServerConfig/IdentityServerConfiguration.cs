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
                    ClientName = "authMicroservice",
                    ClientId= "authMicroservice",
                    ClientSecrets = new [] {new Secret("innoClinicSecret".Sha512())},
                    AllowedGrantTypes = { GrantType.ClientCredentials, GrantType.ResourceOwnerPassword, GrantType.Implicit },
                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.Email,
                        "gatewayAPI.scope", IdentityServerConstants.StandardScopes.OfflineAccess },
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 3600, // 1 hour
                    AccessTokenLifetime = 1200, // 20 minutes
                    AllowRememberConsent = true,
                    RequireConsent = true,
                    RedirectUris = { "http://auth-api:80/.well-known/openid-configuration" }
                }
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
                  new IdentityResources.OpenId(),
                  new IdentityResources.Profile(),
                  new IdentityResources.Email()

          };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource> 
            {
                new ApiResource("gatewayAPI", "Gateway API")
                {
                    Scopes = { "gatewayAPI.scope" },
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> 
            {
                new ApiScope("gatewayAPI.scope", "Gateway API scope")
            };

    }
}
