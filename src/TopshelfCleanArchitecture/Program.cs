using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Topshelf;
using TopshelfCleanArchitecture.Domain.Entities;
using TopshelfCleanArchitecture.Domain.Entities.Base;
using TopshelfCleanArchitecture.Infra.CrossCutting.Ioc;
using TopshelfCleanArchitecture.Infra.CrossCutting.Topshelf.Autofac.Configuration;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels.Base;

namespace TopshelfCleanArchitecture
{
    class Program
    {
        private static IConfigurationRoot _configurationRoot;

        static void Main()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configurationRoot = configurationBuilder.Build();
            _configurationRoot = configurationRoot;

            HostFactory.Run(c =>
            {
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

            ConfigureSerilog(builder);
            //RegisterMaps(builder);

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

        private static void RegisterMaps(ContainerBuilder builder)
        {
            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            var assembliesTypes = assemblyNames
                .SelectMany(an => Assembly.Load(an).GetTypes())
                .Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract)
                .Distinct();

            var autoMapperProfiles = assembliesTypes
                .Select(p => (Profile)Activator.CreateInstance(p)).ToList();

            builder.Register((c, p) =>
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.ForAllMaps((map, expression) =>
                    {
                        foreach (var unmappedPropertyName in map.GetUnmappedPropertyNames())
                            expression.ForMember(unmappedPropertyName,
                                configurationExpression => configurationExpression.Ignore());
                    });

                    foreach (var profile in autoMapperProfiles)
                    {
                        cfg.AddProfile(profile);
                    }

                }).CreateMapper();
            }).InstancePerLifetimeScope();
        }
    }
}
