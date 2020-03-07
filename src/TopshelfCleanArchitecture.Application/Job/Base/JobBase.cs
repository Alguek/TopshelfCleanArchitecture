using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TopshelfCleanArchitecture.Application.Job.Base
{
    [DisallowConcurrentExecution]
    public abstract class JobBase : IJob
    {
        public virtual async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await ExecutarAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public abstract Task ExecutarAsync();
    }
}
