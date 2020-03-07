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

        private static ISessionFactory InitializeSessionFactory(string dbType, string dbVersion, string connString, bool showSql, IList<Type> mappings,
            Action<NHibernate.Cfg.Configuration> configuration)
        {
            var fluentConfiguration = Fluently.Configure();

            IPersistenceConfigurer dbConfig = null;
            switch (dbType.ToLowerInvariant())
            {
                case "mssql":
                    if (showSql)
                        dbConfig = MsSqlConfiguration.MsSql2012.ConnectionString(connString).ShowSql();
                    else
                        dbConfig = MsSqlConfiguration.MsSql2012.ConnectionString(connString);
                    break;
                case "oracle10":
                    if (showSql)
                        dbConfig = OracleDataClientConfiguration.Oracle10.ConnectionString(connString).ShowSql();
                    else
                        dbConfig = OracleDataClientConfiguration.Oracle10.ConnectionString(connString);
                    break;
                case "oracle9":
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

            string dbType = null;
            string dbVersion = null;
            switch (so.DatabaseType)
            {
                case eDatabaseType.mssql_12:
                    dbType = "mssql";
                    dbVersion = "12";
                    break;
                case eDatabaseType.oracle10:
                    dbType = "oracle10";
                    dbVersion = "10";
                    break;
                case eDatabaseType.oracle9:
                    dbType = "oracle9";
                    dbVersion = "9";
                    break;
            }

            return InitializeSessionFactory(dbType, dbVersion, so.ConnectionString, so.ShowSql, so.Mappings, so.ActionConfiguration);
        }
    }
}
