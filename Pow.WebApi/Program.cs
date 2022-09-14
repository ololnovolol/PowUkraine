using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pow.Infrastructure.Migrations;
using Pow.WebApi.Extensions;
using Pow.WebApi.Services;
using System;

namespace Pow.WebApi
{
    public class Program
    {
        
        public static void Main(string[] args)
        {

            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
