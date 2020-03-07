using Autofac;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Linq;
using TopshelfCleanArchitecture.Application.Job.Base;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.IoC
{
    public class JobModule : Module
    {
        private readonly IConfiguration _configuration;
        private readonly string _cronExpression = "0/10 * * * * ?";

        public JobModule(IConfiguration configuration)
        {
            _configuration = configuration;
            _cronExpression = _configuration.GetSection("CronExpression").Value;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JobBase>();
            builder.RegisterType<JobFactory>().As<IJobFactory>().SingleInstance();
            builder.RegisterType<StdSchedulerFactory>().As<ISchedulerFactory>().SingleInstance();

            var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes())
               .Where(p => typeof(JobBase).IsAssignableFrom(p) && p.FullName != "Quartz.IJob" && !p.FullName.Contains("Application.Job.Base.JobBase"));

            foreach (var type in types)
            {
                builder.RegisterType(type).WithParameter("jobId", type.Name).SingleInstance();

                var cronExpression = _configuration.GetSection("Jobs:" + type.Name).Value;

                builder.Register(c => new JobSchedule(
                   jobType: type,
                   cronExpression: cronExpression ?? _cronExpression)).SingleInstance();
            }
        }
    }
}
