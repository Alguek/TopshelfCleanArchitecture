using Autofac;
using AutoMapper;
using System;
using System.Linq;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.IoC
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes())
               .Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract)
               .Distinct();

            var autoMapperProfiles = types
                .Select(p => (Profile)Activator.CreateInstance(p)).ToList();

            builder.Register((c, p) =>
            {
                return new MapperConfiguration(cfg =>
                {
                    foreach (var profile in autoMapperProfiles)
                    {
                        cfg.AddProfile(profile);
                    }
                }).CreateMapper();
            }).InstancePerLifetimeScope();
        }
    }
}
