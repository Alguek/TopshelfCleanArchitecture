using Autofac;
using Quartz;
using System;
using TopshelfCleanArchitecture.Application.Interfaces.Job;

namespace TopshelfCleanArchitecture.Infra.CrossCutting
{
    public class LifetimeScopeApplication : ILifetimeScopeApplication
    {
        private readonly ILifetimeScope _lifetimeScope;

        public LifetimeScopeApplication(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public IJob ResolveJob(Type type)
        {
            return _lifetimeScope.Resolve(type) as IJob;
        }
    }
}
