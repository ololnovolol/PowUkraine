using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class CookieManager
    {
        public static void AddCustomCookiesConfigurations(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(
                config =>
                {
                    config.Cookie.Name = "IdentityServer.Cookie";
                    config.LoginPath = "/Authorization/Login";
                    config.LogoutPath = "/Authorization/Logout";
                });
        }
    }
}
