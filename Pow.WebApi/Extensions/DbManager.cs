using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pow.Infrastructure;
using Pow.Infrastructure.Context;
using Pow.Infrastructure.Migrations;

namespace Pow.WebApi.Extensions
{
    public static class DbManager
    {
        public static void AddCustomDapperConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DapperContext>();
            services.AddSingleton<Database>();

            services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    c => c.AddSqlServer()
                        .WithGlobalConnectionString(configuration.GetConnectionString("DbConnection"))
                        .ScanIn(typeof(DbInitialization).Assembly)
                        .For.Migrations());
        }
    }
}
