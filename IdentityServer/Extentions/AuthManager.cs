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
                    google.ClientId = AppConfiguration["google:client_id"];
                    google.ClientSecret = AppConfiguration["google:client_secret"];
                })
                .AddFacebook(facebook =>
                {
                    facebook.AppId = AppConfiguration["facebook:client_id"];
                    facebook.AppSecret = AppConfiguration["facebook:client_secret"];
                })
                .AddLinkedIn(linkedIn =>
                {
                    linkedIn.ClientId = AppConfiguration["linkedin:client_id"];
                    linkedIn.ClientSecret = AppConfiguration["linkedin:client_secret"];
                });
        }
    }




}
