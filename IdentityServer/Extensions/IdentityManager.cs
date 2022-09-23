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
                    config =>
                    {
                        config.Password.RequireUppercase = true;
                        config.Password.RequireDigit = true;
                        config.Password.RequireLowercase = true;
                        config.Password.RequiredLength = 8;
                        config.User.RequireUniqueEmail = true;
                        config.User.AllowedUserNameCharacters = "";
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