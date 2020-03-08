using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using TopshelfCleanArchitecture.Application.UseCase.Base;
using TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts;
using TopshelfCleanArchitecture.Infra.CrossCutting.MediatR;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.IoC
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddMediatR(typeof(MediatRBase).Assembly);

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IAsyncRequestHandler)
            // in assembly holding the Commands
            builder.RegisterAssemblyTypes(
                                  typeof(ReturnListOfProdutsCommand).GetTypeInfo().Assembly).
                                       AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterType<ILogger>();

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).
                                                       As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).
                                                       As(typeof(IPipelineBehavior<,>));
        }
    }
}
