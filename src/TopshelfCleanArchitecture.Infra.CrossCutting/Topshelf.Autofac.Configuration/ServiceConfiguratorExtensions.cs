using Autofac;
using Topshelf.ServiceConfigurators;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.Topshelf.Autofac.Configuration
{
    public static class ServiceConfiguratorExtensions
    {
        public static ServiceConfigurator<T> ConstructUsingAutofacContainer<T>(this ServiceConfigurator<T> configurator) where T : class
        {
            configurator.ConstructUsing(serviceFactory => AutofacHostBuilderConfigurator.LifetimeScope.Resolve<T>());
            return configurator;
        }
    }
}
