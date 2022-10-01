using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> IdentityResources
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", new[] { "role" }),
            };

        public static IEnumerable<ApiScope> ApiScopes(IConfiguration config)
        {
            return new List<ApiScope>
            {
                new ApiScope(config["clientSecret:c_name"], config["clientSecret:c_Disp_name"]),
            };
        }

        public static IEnumerable<ApiResource> ApiResources(IConfiguration config)
        {
            return new List<ApiResource>
            {
                new ApiResource(
                    config["clientSecret:c_name"],
                    config["clientSecret:c_Disp_name"],
                    new[] { JwtClaimTypes.Name }) { Scopes = { config["clientSecret:c_name"] } },
            };
        }

        public static IEnumerable<Client> Clients(IConfiguration config)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = config["clientSecret:c_id"],
                    ClientName = "Pow Web",
                    ClientSecrets = { new Secret(config["clientSecret:c_id"].ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RequireConsent = false,
                    FrontChannelLogoutUri = config["clientSecret:c_front_logout_uri"],
                    RedirectUris = { config["clientSecret:c_redirect_uris"] },
                    AllowedCorsOrigins = { config["clientSecret:c_cors_origin"] },
                    PostLogoutRedirectUris = { config["clientSecret:c_post_logout"] },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        config["clientSecret:c_name"],
                    },
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 1,
                },
            };
        }
    }
}
