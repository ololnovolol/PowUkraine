using Microsoft.Extensions.DependencyInjection;
using Pow.Infrastructure.Repositories.Interfaces;
using Pow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Pow.WebApi.Extensions
{
    public static class AuthManager
    {
        public static void AddCustomAuthConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:44316/";
                    options.Audience = "PowWebApi";
                    options.RequireHttpsMetadata = false;
                });
        }
    }
}
