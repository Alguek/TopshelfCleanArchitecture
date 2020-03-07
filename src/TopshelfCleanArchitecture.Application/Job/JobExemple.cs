using MediatR;
using Quartz;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces;
using TopshelfCleanArchitecture.Application.Interfaces.Repository;
using TopshelfCleanArchitecture.Application.Job.Base;
using TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts;

namespace TopshelfCleanArchitecture.Application.Job
{
    [DisallowConcurrentExecution]
    public class JobExemple : JobBase
    {
        private readonly IMediator _mediator;

        public JobExemple(string jobId, ILoggerFile loggerFile,  IMediator mediator) : base(jobId, loggerFile)
        {
            _mediator = mediator;
        }

        public async override Task ExecutarAsync()
        {
            var testMediator = await _mediator.Send(new ReturnListOfProdutsCommand());
        }
    }
}
