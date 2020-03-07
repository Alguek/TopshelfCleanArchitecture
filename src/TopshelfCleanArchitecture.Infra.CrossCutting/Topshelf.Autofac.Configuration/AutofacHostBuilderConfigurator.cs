using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Topshelf.Builders;
using Topshelf.Configurators;
using Topshelf.HostConfigurators;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.Topshelf.Autofac.Configuration
{
    public class AutofacHostBuilderConfigurator : HostBuilderConfigurator
    {
        private static ILifetimeScope _lifetimeScope;

        public static ILifetimeScope LifetimeScope
        {
            get
            {
                return _lifetimeScope;
            }
        }

        public AutofacHostBuilderConfigurator(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope ?? throw new ArgumentNullException("lifetimeScope");
        }


        public HostBuilder Configure(HostBuilder builder)
        {
            return builder;
        }

        public IEnumerable<ValidateResult> Validate()
        {
            yield break;
        }

    }
}
