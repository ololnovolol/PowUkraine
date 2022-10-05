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
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await userManager.AddToRoleAsync(await CreateUserAsync("owner", configuration, userManager), "Admin");
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
            AppUser result = await userManager.FindByNameAsync(name);

            if (result != null)
            {
                return result;
            }

            AppUser user = new AppUser();
            user.UserName = configuration[$"{name}:name"];
            user.Email = configuration[$"{name}:email"];
            user.BirthDay = DateTime.Parse(configuration[$"{name}:bDay"]);

            await userManager.CreateAsync(user, configuration[$"{name}:password"]);

            return user;
        }
    }
}
