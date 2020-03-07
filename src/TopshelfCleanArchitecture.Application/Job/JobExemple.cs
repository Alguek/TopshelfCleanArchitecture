using Quartz;
using System;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces;
using TopshelfCleanArchitecture.Application.Job.Base;

namespace TopshelfCleanArchitecture.Application.Job
{
    [DisallowConcurrentExecution]
    public class JobExemple : JobBase
    {
        public JobExemple(string jobId, ILoggerFile loggerFile) : base(jobId, loggerFile)
        {

        }

        public override Task ExecutarAsync()
        {
            throw new NotImplementedException();
        }
    }
}
