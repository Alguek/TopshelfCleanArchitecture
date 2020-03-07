using System;
using System.Collections.Generic;
using System.Text;
using TopshelfCleanArchitecture.Domain.Enum;

namespace TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess
{
    public class SessionFactoryConfiguration
    {
        public EDatabaseType DatabaseType { get; set; }

        public string ConnectionString { get; set; }

        /// <summary>
        /// If enabled, NHibernate will output all SQL commands executed.
        /// </summary>
        public bool ShowSql = false;

        public void AddFluentMapping(Type mapping)
        {
            Mappings.Add(mapping);
        }

        public IList<Type> Mappings { get; } = new List<Type>();

        /// <summary>
        /// Configurações customizadas
        /// </summary>
        public void Configuration(Action<NHibernate.Cfg.Configuration> configuration)
        {
            ActionConfiguration = configuration;
        }

        /// <summary>
        /// Configurações customizadas
        /// </summary>
        internal Action<NHibernate.Cfg.Configuration> ActionConfiguration { get; private set; }
    }
}
