using Autofac;
using Topshelf.HostConfigurators;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.TopshelfAutofacConfiguration
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
