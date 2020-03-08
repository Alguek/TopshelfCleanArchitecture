﻿using Autofac;
using MediatR;
using TopshelfCleanArchitecture.Application.Interfaces.Job;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.IoC
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
               .RegisterAssemblyTypes(typeof(LifetimeScopeApplication).Assembly)
               .AsImplementedInterfaces().InstancePerLifetimeScope();

             builder
               .RegisterAssemblyTypes(typeof(IQuartzHostedService).Assembly)
               .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
