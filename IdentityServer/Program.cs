using IdentityServer.Extentions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace IdentityServer
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            LoggerManager.RunSerilog();
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
