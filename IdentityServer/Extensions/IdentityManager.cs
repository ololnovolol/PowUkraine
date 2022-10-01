using IdentityServer.Common.Validators;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class IdentityManager
    {
        public static void AddCustomIdentityConfigurations(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<AppUser, IdentityRole>(
                    identityOptions =>
                    {
                        identityOptions.Password.RequireUppercase = true;
                        identityOptions.Password.RequireDigit = true;
                        identityOptions.Password.RequireLowercase = true;
                        identityOptions.Password.RequiredLength = 8;
                        identityOptions.User.RequireUniqueEmail = true;
                        identityOptions.User.AllowedUserNameCharacters = string.Empty;
                    })
                .AddEntityFrameworkStores<AuthorizationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiResources(Configuration.ApiResources(config))
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes(config))
                .AddInMemoryClients(Configuration.Clients(config))
                .AddDeveloperSigningCredential();

            services.AddTransient<IUserValidator<AppUser>, CustomUserValidator>();
        }
    }
}
