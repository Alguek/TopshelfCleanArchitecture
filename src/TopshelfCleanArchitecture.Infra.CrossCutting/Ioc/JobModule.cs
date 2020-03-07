using Autofac;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Linq;
using TopshelfCleanArchitecture.Application.Job.Base;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.Ioc
{
    public class JobModule : Module
    {
        private readonly string _cronExpression = "0/10 * * * * ?";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JobBase>();
            builder.RegisterType<JobFactory>().As<IJobFactory>().SingleInstance();
            builder.RegisterType<StdSchedulerFactory>().As<ISchedulerFactory>().SingleInstance();

            var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes())
               .Where(p => typeof(JobBase).IsAssignableFrom(p) && p.FullName != "Quartz.IJob" && p.FullName != "TopshelfCleanArchitecture.Application.Job.Base.JobBase");

            foreach (var type in types)
            {
                builder.RegisterType(type).SingleInstance();

                builder.Register(c => new JobSchedule(
                   jobType: type,
                   cronExpression: _cronExpression)).SingleInstance();
            }
        }
    }
}
