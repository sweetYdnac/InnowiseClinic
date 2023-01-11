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
            new ApiScope[]
            { 
                new ApiScope("read", "Read"),
                new ApiScope("write", "Write")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            { 
                new Client
                {
                    ClientId = "client",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = { "read" }
                }
            };
    }
}
