using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces;

namespace TopshelfCleanArchitecture.Application.Job.Base
{
    [DisallowConcurrentExecution]
    public abstract class JobBase : IJob
    {
        protected readonly ILoggerFile _loggerFile;
        protected readonly string _jobId;
        private Stopwatch _stopwatch;

        public JobBase(string jobId, ILoggerFile loggerFile)
        {
            _loggerFile = loggerFile;
            _jobId = jobId;
        }

        public virtual async Task Execute(IJobExecutionContext context)
        {
            try
            {
                LogStart();
                await ExecutarAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                LogEnd();
            }

        }

        protected void LogStart()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _loggerFile.Information(string.Format("[INICIO] '{0}'", _jobId));
        }

        protected void LogEnd()
        {
            _stopwatch.Stop();
            _loggerFile.Information((string.Format("[FIM] '{0}' {1} ms de execução", _jobId, _stopwatch.ElapsedMilliseconds)));
        }

        public abstract Task ExecutarAsync();
    }
}
