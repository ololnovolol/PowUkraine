using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class AuthManager
    {
        public static void AddCustomAuthenticationConfigurations(
            this IServiceCollection services,
            IConfiguration appConfiguration)
        {
            services.AddAuthentication()
                .AddGoogle(
                    google =>
                    {
                        google.ClientId = appConfiguration["google:client_id"];
                        google.ClientSecret = appConfiguration["google:client_secret"];
                    })
                .AddFacebook(
                    facebook =>
                    {
                        facebook.AppId = appConfiguration["facebook:client_id"];
                        facebook.AppSecret = appConfiguration["facebook:client_secret"];
                    })
                .AddLinkedIn(
                    linkedIn =>
                    {
                        linkedIn.ClientId = appConfiguration["linkedin:client_id"];
                        linkedIn.ClientSecret = appConfiguration["linkedin:client_secret"];
                    });
        }
    }
}
