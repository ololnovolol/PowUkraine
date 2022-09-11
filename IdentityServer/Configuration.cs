using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("PowWebApi", "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("PowWebApi", "Web API", new[]
                    { JwtClaimTypes.Name })
                {
                    Scopes = {"PowWebAPI"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client()
                {
                    ClientId = "pow-web-api",
                    ClientName = "Pow Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false, // todo set code
                    RequirePkce = true,
                    RedirectUris = {"http:// .../signin-oidc"}, // todo setup with frontend
                    AllowedCorsOrigins =
                    {
                        "http:/..." // todo setup with frontend and any other
                    },
                    PostLogoutRedirectUris =
                    {
                        "http:/.../signout-oidc" // todo setup with front end
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OpenId,
                        "PowWebApi"

                    },
                    AllowAccessTokensViaBrowser = true
                }
            };

    }
}
