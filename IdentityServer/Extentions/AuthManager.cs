using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extentions
{
    public static class AuthManager
    {
        public static void AddCustomAuthenticationConfigurations(this IServiceCollection services, IConfiguration AppConfiguration)
        {
            services.AddAuthentication()
                .AddGoogle(google =>
                {
                    google.ClientId = AppConfiguration["web:client_id"];
                    google.ClientSecret = AppConfiguration["web:client_secret"];
                });
        }
    }



}
