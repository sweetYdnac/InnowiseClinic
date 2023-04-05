using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Authorization.API.Extensions
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                     Name = "Role",
                     UserClaims = { JwtClaimTypes.Role }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("Read", "Read"),
                new ApiScope("Write", "Write"),
                new ApiScope("Full", "Full"),
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "machineClient",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        "Full",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                },

                new Client
                {
                    ClientId = "userClient",
                    RequireClientSecret = false,
                    AllowedGrantTypes = new string[]{
                        GrantType.ResourceOwnerPassword,
                        "refresh_token",
                    },
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        "Full",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    },
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = 60 * 60 * 24 * 14,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 60 * 15,
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource()
                {
                    Name = "Full",
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Role },
                    Scopes =
                    {
                        "Full",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    },
                }
            };
    }
}
