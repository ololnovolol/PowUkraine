using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Pow.WebApi.Extensions
{
    public static class LoggerManager
    {
        public static IHost SetupLogger(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    // todo add need scopes
                }
                catch (Exception exception)
                {
                    Log.Fatal(exception, "An error occurred while app Initialization");
                }
            }

            return host;
        }

        public static void RunLogger()
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            Log.Logger = new LoggerConfiguration()

                // .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    "./LogData/Pow_Api_WebLog.txt",
                    rollingInterval:
                    RollingInterval.Day)
                .CreateLogger();
        }
    }
}
