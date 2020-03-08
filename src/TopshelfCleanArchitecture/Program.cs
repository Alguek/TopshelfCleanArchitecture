﻿using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using Topshelf;
using TopshelfCleanArchitecture.Infra.CrossCutting.IoC;
using TopshelfCleanArchitecture.Infra.CrossCutting.TopshelfAutofacConfiguration;

namespace TopshelfCleanArchitecture
{
    class Program
    {
        private static IConfigurationRoot _configurationRoot;

        static void Main()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true);

            var configurationRoot = configurationBuilder.Build();
            _configurationRoot = configurationRoot;

            HostFactory.Run(c =>
            {
                c.SetServiceName(": TopshelfCleanArchitecture");
                c.SetDisplayName(": TopshelfCleanArchitecture");
                c.SetDescription(": A exemple of a service using Topshelf with Clean Architecture");

                c.UseAutofacContainer(ConfigureContainer());

                c.Service<Startup>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted(async w => await w.StartAsync());
                    s.WhenStopped(async w => await w.StopAsync());
                });
            });

        }

        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Startup>();
            builder.RegisterModule(new JobModule(_configurationRoot));
            builder.RegisterModule(new InfraModule(_configurationRoot));
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new AutoMapperModule());
            builder.RegisterModule(new MediatorModule());

            ConfigureSerilog(builder);

            var container = builder.Build();

            return container;
        }

        static void ConfigureSerilog(ContainerBuilder builder)
        {
            builder.Register<ILogger>((c, p) =>
            {
                return new LoggerConfiguration()
                    .ReadFrom.Configuration(_configurationRoot)
                    .CreateLogger();
            }).SingleInstance();
        }
    }
}
