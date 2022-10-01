using System;
using IdentityServer.Data;
using IdentityServer.Extensions;
using IdentityServer.Services;
using IdentityServer4.Services;
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
        private readonly IConfiguration _appConfiguration;

        public Startup(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AuthorizationDbContext>(
                options => { options.UseSqlServer(_appConfiguration.GetValue<string>("DbConnection")); });

            services.AddCustomCorsConfiguration();
            services.AddCustomIdentityConfigurations(_appConfiguration);
            services.AddCustomCookiesConfigurations();
            services.AddCustomAuthenticationConfigurations(_appConfiguration);
            services.AddTransient<IProfileService, ProfileService>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, IServiceProvider services)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseIdentityServer();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

            RolesConfigurationService.CreateUserRoles(services, _appConfiguration).Wait();
        }
    }
}
