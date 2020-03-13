using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace TopshelfCleanArchitecture.Infra.Migrations
{
    class Program
    {
        private static IConfigurationRoot _configurationRoot;

        static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            _configurationRoot = configurationBuilder.Build();

            var serviceProvider = CreateServices();

            using var scope = serviceProvider.CreateScope();
            if (!args.Any())
            {
                UpDatabase(scope.ServiceProvider);
                Console.WriteLine("Migration Up complete!");
            }
            else if (args[0].ToUpper() == "-DOWN")
            {
                DownDatabase(scope.ServiceProvider);
                Console.WriteLine("Migration Down complete!");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => ConfigureDatabaseType(rb))
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        static void ConfigureDatabaseType(IMigrationRunnerBuilder config)
        {
            var databaseType = _configurationRoot.GetSection("ConnectionStrings").GetSection("DatabaseType").Value;

            config
                .WithGlobalConnectionString(_configurationRoot.GetConnectionString("DefaultConnection"))
                .ScanIn(typeof(Baseline).Assembly).For.Migrations();

            switch (databaseType)
            {
                case "mssql_12":
                    config.AddSqlServer();
                    break;
                case "oracle9":
                case "oracle10":
                    config.AddOracle();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Up the database
        /// </summary>
        private static void UpDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }

        /// <summary>
        /// Down the database
        /// </summary>
        private static void DownDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.Rollback(1);
        }
    }
}
