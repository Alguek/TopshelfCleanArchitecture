using Autofac;
using TopshelfCleanArchitecture.Application.Interfaces.Job;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.Ioc
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
