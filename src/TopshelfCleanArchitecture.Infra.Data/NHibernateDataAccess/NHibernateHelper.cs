using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Reflection;
using TopshelfCleanArchitecture.Domain.Enum;

namespace TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess
{
    public static class NHibernateHelper
    {
        private static void ConfigureAssemblies(ref FluentConfiguration configuration)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var attribute in assembly.GetCustomAttributes(true))
                {
                    if (attribute is HibernatePersistenceAttribute)
                    {
                        Assembly bgmAssembly = assembly;
                        configuration.Mappings(m => m.FluentMappings.AddFromAssembly(bgmAssembly));
                    }
                }
            }
        }

        private static ISessionFactory InitializeSessionFactory(EDatabaseType dbType, string connString, bool showSql, IList<Type> mappings,
            Action<NHibernate.Cfg.Configuration> configuration)
        {
            var fluentConfiguration = Fluently.Configure();

            IPersistenceConfigurer dbConfig = null;
            switch (dbType)
            {
                case EDatabaseType.mssql_12:
                    if (showSql)
                        dbConfig = MsSqlConfiguration.MsSql2012.ConnectionString(connString).ShowSql();
                    else
                        dbConfig = MsSqlConfiguration.MsSql2012.ConnectionString(connString);
                    break;
                case EDatabaseType.oracle10:
                    if (showSql)
                        dbConfig = OracleDataClientConfiguration.Oracle10.ConnectionString(connString).ShowSql();
                    else
                        dbConfig = OracleDataClientConfiguration.Oracle10.ConnectionString(connString);
                    break;
                case EDatabaseType.oracle9:
                    if (showSql)
                        dbConfig = OracleDataClientConfiguration.Oracle9.ConnectionString(connString).ShowSql();
                    else
                        dbConfig = OracleDataClientConfiguration.Oracle9.ConnectionString(connString);
                    break;
            }

            fluentConfiguration.Mappings(m =>
            {
                foreach (var sessionOptionsMapping in mappings)
                {
                    m.FluentMappings.Add(sessionOptionsMapping);
                }
            });

            fluentConfiguration.Database(dbConfig);
            fluentConfiguration.CurrentSessionContext("web");

            if (configuration != null)
                fluentConfiguration.ExposeConfiguration(configuration);
            
            ConfigureAssemblies(ref fluentConfiguration);            

            return fluentConfiguration.BuildSessionFactory();
        }

        public static ISessionFactory ConfigureSessionFactory(Action<SessionFactoryConfiguration> Configuration)
        {
            var so = new SessionFactoryConfiguration();
            Configuration(so);
            if (string.IsNullOrEmpty(so.ConnectionString))
                throw new ArgumentNullException("String de conexão não pode ser null");            

            return InitializeSessionFactory(so.DatabaseType, so.ConnectionString, so.ShowSql, so.Mappings, so.ActionConfiguration);
        }
    }
}
