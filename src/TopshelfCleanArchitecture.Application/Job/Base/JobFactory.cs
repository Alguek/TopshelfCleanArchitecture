using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using TopshelfCleanArchitecture.Application.Interfaces.Job;

namespace TopshelfCleanArchitecture.Application.Job.Base
{
    public class JobFactory : IJobFactory
    {
        private readonly ILifetimeScopeApplication _lifetimeScopeApplication;

        public JobFactory(ILifetimeScopeApplication lifetimeScopeApplication)
        {
            _lifetimeScopeApplication = lifetimeScopeApplication;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var job = _lifetimeScopeApplication.ResolveJob(bundle.JobDetail.JobType);
            if (job == null)
                throw new NotSupportedException($"{bundle.JobDetail.JobType.Name} is not supported!");

            return job;
        }

        public void ReturnJob(IJob job)
        {
            if (job is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
