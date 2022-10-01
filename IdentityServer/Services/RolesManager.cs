using System;
using System.Threading.Tasks;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Services
{
    public static class RolesConfigurationService
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await userManager.AddToRoleAsync(await CreateUserAsync("admin", configuration, userManager), "Admin");
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
                await userManager.AddToRoleAsync(await CreateUserAsync("user", configuration, userManager), "User");
            }
        }

        private static async Task<AppUser> CreateUserAsync(
            string name,
            IConfiguration configuration,
            UserManager<AppUser> userManager)
        {
            var result = await userManager.FindByNameAsync(name);

            if (result != null)
            {
                return result;
            }

            var user = new AppUser { UserName = name, Email = configuration[$"{name}:email"], BirthDay = DateTime.Now };

            await userManager.CreateAsync(user, configuration[$"{name}:password"]);

            return user;
        }
    }
}
