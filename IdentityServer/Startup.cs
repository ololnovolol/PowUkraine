using IdentityServer.Data;
using IdentityServer.Extentions;
using IdentityServer.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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

            services.AddTransient<IProfileService, ProfileService>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
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

            RolesConfigurationService.CreateUserRoles(services, AppConfiguration).Wait();
        }


    }
}
