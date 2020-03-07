﻿using Autofac;
using Microsoft.Extensions.Configuration;
using System.Linq;
using TopshelfCleanArchitecture.Domain.Enum;
using TopshelfCleanArchitecture.Domain.Models.Configuration;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.Repositories.Base;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.Ioc
{
    public class InfraModule : Module
    {
        private readonly IConfiguration _configuration;
        public InfraModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
               .RegisterAssemblyTypes(typeof(Repository<,>).Assembly)
               .AsImplementedInterfaces().InstancePerLifetimeScope();

            var configuration = _configuration.GetSection("ConnectionStrings").Get<ConnectionStringsModel>();

            var sessionFactory = NHibernateHelper.ConfigureSessionFactory(a =>
            {
                a.ConnectionString = configuration.DefaultConnection;
                a.DatabaseType = configuration.DatabaseType;
                a.ShowSql = true;
                a.Configuration(c =>
                {
                    if (configuration.DatabaseType == eDatabaseType.oracle10 || (configuration.DatabaseType == eDatabaseType.oracle9))
                        c.SetProperty("connection.driver_class", "NHibernate.Driver.OracleManagedDataClientDriver");
                });
            });

            using(var session = sessionFactory.OpenSession())
            {
                var teste = session.Query<ProductData>().ToList();
            }

            builder.Register(f => sessionFactory).SingleInstance();
        }
    }
}