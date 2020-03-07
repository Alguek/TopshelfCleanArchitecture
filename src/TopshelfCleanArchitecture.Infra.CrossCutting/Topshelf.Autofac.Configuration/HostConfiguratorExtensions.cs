using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Topshelf.HostConfigurators;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.Topshelf.Autofac.Configuration
{
   public static class HostConfiguratorExtensions
    {
        public static HostConfigurator UseAutofacContainer(this HostConfigurator configurator, ILifetimeScope lifetimeScope)
        {
            configurator.AddConfigurator(new AutofacHostBuilderConfigurator(lifetimeScope));
            return configurator;
        }
    }
}
