using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces;
using TopshelfCleanArchitecture.Application.Job.Base;

namespace TopshelfCleanArchitecture.Application.Job
{
    public class JobExemple : JobBase
    {
        public JobExemple(ILoggerFile loggerFile)
        {
            loggerFile.Information("teste");
        }

        public override Task ExecutarAsync()
        {
            throw new NotImplementedException();
        }
    }
}
