using Autofac;
using System;
using System.Collections.Generic;
using Topshelf.Builders;
using Topshelf.Configurators;
using Topshelf.HostConfigurators;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.TopshelfAutofacConfiguration
{
    /// <summary>
    /// All the credits for alexandrnikitin
    /// https://github.com/alexandrnikitin/Topshelf.Autofac
    /// </summary>
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
