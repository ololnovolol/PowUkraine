using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pow.Infrastructure;

namespace Pow.WebApi.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                Database databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                IMigrationRunner migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                databaseService.CreateDatabase("IdentityServer");
                migrationService.ListMigrations();
                migrationService.MigrateUp();
            }

            return host;
        }
    }
}
