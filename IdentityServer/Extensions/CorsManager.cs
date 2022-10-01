using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class CorsManager
    {
        public static void AddCustomCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        "AllowAll",
                        policy =>
                        {
                            policy.AllowAnyHeader();
                            policy.AllowAnyMethod();
                            policy.AllowAnyOrigin();
                        });
                });
        }
    }
}
