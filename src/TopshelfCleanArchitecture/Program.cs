using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO;
using Topshelf;
using TopshelfCleanArchitecture.Infra.CrossCutting.Ioc;
using TopshelfCleanArchitecture.Infra.CrossCutting.Topshelf.Autofac.Configuration;

namespace TopshelfCleanArchitecture
{
    class Program
    {
        private static IConfigurationRoot _configurationRoot;

        static void Main(string[] args)
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
            builder.RegisterModule(new ApplicationModule());

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
