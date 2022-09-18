using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public static class RolesConfigurationService
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!await RoleManager.RoleExistsAsync("Admin"))
            {
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
                await UserManager.AddToRoleAsync(await CreateUserAsync("admin", configuration, UserManager), "Admin");
            }

            if (!await RoleManager.RoleExistsAsync("User"))
            {
                await RoleManager.CreateAsync(new IdentityRole("User"));
                await UserManager.AddToRoleAsync(await CreateUserAsync("user", configuration, UserManager), "User");
            }

        }

        private static async Task<AppUser> CreateUserAsync(string name, IConfiguration configuration,
            UserManager<AppUser> userManager)
        {
            var result = await userManager.FindByNameAsync(name);

            if (result == null)
            {
                var user = new AppUser
                {
                    UserName = name,
                    Email = configuration[$"{name}:email"],
                    BirthDay = DateTime.Now,
                };

                await userManager.CreateAsync(user, configuration[$"{name}:password"]);
                return user;
            }

            return result;
        }

    }
}
