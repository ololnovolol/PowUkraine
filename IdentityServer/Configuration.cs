﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

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
                new IdentityResources.Profile(),
                new IdentityResource("roles", new[] { "role" })
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("PowWebApi", "Web API", new[]
                    { JwtClaimTypes.Name })
                {
                    Scopes = { "PowWebApi" }
                }
            };

        //// TODO Move links and etc to .config file
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client()
                {
                    ClientId = "pow-web-app",
                    ClientName = "Pow Web",
                    ClientSecrets = { new Secret("palyanitsa_=)".ToSha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false, // todo set code
                    RequirePkce = true,
                    RequireConsent = false,
                    FrontChannelLogoutUri = "https://localhost:3000/signout-oidc", // todo setup with frontend
                    RedirectUris = {"http://localhost:3000/signin-oidc"}, // todo setup with frontend
                    AllowedCorsOrigins =
                    {
                        "http://localhost:3000" // todo setup with frontend and any other
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:3000/signout-callback-oidc" // todo setup with front end
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "PowWebApi"
                    },
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 1
                }
            };

    }
}
