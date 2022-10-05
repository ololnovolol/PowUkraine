using System;
using System.Diagnostics;
using IdentityServer.Data;
using IdentityServer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace IdentityServer.Extensions
{
    public static class LoggerManager
    {
        [Obsolete]
        public static IHost SetupLogger(this IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                try
                {
                    IHostingEnvironment environment = serviceProvider.GetRequiredService<IHostingEnvironment>();

                    if (!environment.IsDevelopment())
                    {
                        AuthorizationDbContext context = serviceProvider.GetRequiredService<AuthorizationDbContext>();
                        DbInitializer.Initialize(context);
                    }
                }
                catch (Exception exception)
                {
                    Log.Fatal(exception, ResourceReader.GetExceptionMessage("occurred_initialize"));
                }
            }

            return host;
        }

        public static void RunLogger()
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    "./LogData/Pow_Identity_WebLog.txt",
                    rollingInterval:
                    RollingInterval.Day)
                .CreateLogger();
        }
    }
}
