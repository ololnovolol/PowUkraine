using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Pow.Persistance.Migrations;

namespace Pow.Persistance.Runner
{
    static class Program
    {
        static void Main()
        {
            var serviceProvider = CreateServices();
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString("Server=(localdb)\\MSSQLLocalDB;Database=IdentityServer;Trusted_Connection=True")
                    .ScanIn(typeof(DBInitialization).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
