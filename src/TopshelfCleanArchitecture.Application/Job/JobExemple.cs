using MediatR;
using Quartz;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces;
using TopshelfCleanArchitecture.Application.Job.Base;
using TopshelfCleanArchitecture.Application.Resources;
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
            //The result is encapsulated on a Class (ResponseBase) that returns the result from the usecase or errors from de validation
            ResultBase<List<ReturnListOfProdutsResponse>> resultWithoutError = await _mediator.Send(new ReturnListOfProdutsRequest() { Id = 1 });
            _loggerFile.Information(Messages.OkResultUsecaseConsole, resultWithoutError);

            ResultBase<List<ReturnListOfProdutsResponse>> resultWithError = await _mediator.Send(new ReturnListOfProdutsRequest() { Id = 0 });
            _loggerFile.Information(Messages.FailResultUsecaseConsole, resultWithError);
        }
    }
}
