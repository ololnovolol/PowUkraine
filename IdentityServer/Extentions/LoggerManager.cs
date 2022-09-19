using IdentityServer.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;

namespace IdentityServer.Extentions
{
    public static class LoggerManager
    {
        [Obsolete]
        public static IHost SetupLogger(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<AuthorizationDbContext>();
                    DbInitializer.Initialize(context);

                    var environment = serviceProvider.GetRequiredService<IHostingEnvironment>();

                    if (!environment.IsDevelopment())
                    {
                        context = serviceProvider.GetRequiredService<AuthorizationDbContext>();
                        DbInitializer.Initialize(context);
                    }
                }
                catch (Exception exception)
                {
                    Log.Fatal(exception, "An error occurred while app Initialization");
                }
            }

            return host;
        }


        public static void RunSerilog()
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            Log.Logger = new LoggerConfiguration()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File("./LogData/Pow_Identity_WebLog.txt", rollingInterval:
                    RollingInterval.Day)
                .CreateLogger();
        }
    }
}
