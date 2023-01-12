using IdentityServer4.Models;

namespace Authorization.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            { 
                new ApiScope("AuthorizationAPI.Read", "Read"),
                new ApiScope("AuthorizationAPI.Write", "Write")
            };

        public static IEnumerable<Client> Clients =>
            new[]
            { 
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedScopes = { "AuthorizationAPI.Read", "AuthorizationAPI.Write" }
                },
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "AuthorizationAPI.Read" },
                    RequirePkce = true,
                    RequireConsent= true,
                    AllowPlainTextPkce= true,
                    RedirectUris = { "https://https://localhost:44306/signin-oidc" },
                    FrontChannelLogoutUri = "https://https://localhost:44306/signout-oidc",
                    PostLogoutRedirectUris= { "https://https://localhost:44306/signout-callback-oidc" },
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("Authorization.API")
                {
                    Scopes = { "AuthorizationAPI.Read", "AuthorizationAPI.Write" },
                    ApiSecrets = { new Secret("AuthSecret".Sha256()) },
                    UserClaims = { "role" }
                }
            };
    }
}
