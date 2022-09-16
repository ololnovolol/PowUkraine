using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extentions
{
    public static class IdentityManager
    {
        public static void AddCustomIdentityConfigurations(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                config.Password.RequireUppercase = true;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequiredLength = 8;
                config.User.RequireUniqueEmail = true;

            })
           .AddEntityFrameworkStores<AuthorizationDbContext>()
           .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();
        }
    }
}
