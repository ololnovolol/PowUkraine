using IdentityServer.Data;
using IdentityServer.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; }

        public Startup(IConfiguration appConfiguration)
        {
            AppConfiguration = appConfiguration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseSqlServer(AppConfiguration.GetValue<string>("DbConnection"));
            });

            services.AddCustomIdentityConfigurations();

            services.AddCustomCookiesConfigurations();

            services.AddCustomAuthenticationConfigurations(AppConfiguration);

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
