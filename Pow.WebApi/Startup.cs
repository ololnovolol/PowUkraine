using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pow.Infrastructure;
using Pow.Infrastructure.Context;
using Pow.Infrastructure.Migrations;
using Pow.WebApi.Extensions;
using Pow.WebApi.Middleware;

namespace Pow.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddSingleton<Database>();
            services.AddLogging(c => c.AddFluentMigratorConsole())
                 .AddFluentMigratorCore()
                 .ConfigureRunner(c => c.AddSqlServer()
                 .WithGlobalConnectionString(Configuration.GetConnectionString("DbConnection"))
                 .ScanIn(typeof(DBInitialization).Assembly).For.Migrations());


            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

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
            services.AddInfrastructure();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
     
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll"); // todo expand Cors
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
