﻿using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
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
            builder.AddMediatR(typeof(ResponseBase).Assembly);

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(
                                  typeof(ReturnListOfProdutsRequest).GetTypeInfo().Assembly).
                                       AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).
                                                       As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).
                                                       As(typeof(IPipelineBehavior<,>));
        }
    }
}