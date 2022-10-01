using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Pow.WebApi.Extensions;
using Serilog;

namespace Pow.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            LoggerManager.RunLogger();

            try
            {
                Log.Information("Starting host...");

                CreateHostBuilder(args)
                    .Build()
                    .SetupLogger()
                    .MigrateDatabase()
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
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
