using System;
using IdentityServer.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace IdentityServer
{
    public static class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            LoggerManager.RunLogger();

            try
            {
                Log.Information("Starting host...");

                CreateHostBuilder(args)
                    .Build()
                    .SetupLogger()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
