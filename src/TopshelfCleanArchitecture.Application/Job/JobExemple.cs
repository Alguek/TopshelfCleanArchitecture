using MediatR;
using Quartz;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces;
using TopshelfCleanArchitecture.Application.Job.Base;
using TopshelfCleanArchitecture.Application.UseCase.Base;
using TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts;

namespace TopshelfCleanArchitecture.Application.Job
{
    [DisallowConcurrentExecution]
    public class JobExemple : JobBase
    {
        private readonly IMediator _mediator;

        public JobExemple(string jobId, ILoggerFile loggerFile, IMediator mediator) : base(jobId, loggerFile)
        {
            _mediator = mediator;
        }

        public async override Task ExecutarAsync()
        {
            ResponseBase<List<ReturnListOfProdutsResponse>> testMediator1 = await _mediator.Send(new ReturnListOfProdutsRequest() { Id = 1 });

            var testMediator2 = await _mediator.Send(new ReturnListOfProdutsRequest() { Id = 1 });
        }
    }
}
