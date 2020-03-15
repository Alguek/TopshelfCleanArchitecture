using MediatR;
using System.Collections.Generic;
using TopshelfCleanArchitecture.Application.UseCase.Base;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts
{
    public class ReturnListOfProdutsRequest : IRequest<ResultBase<List<ReturnListOfProdutsResponse>>>
    {
        public int Id { get; set; }
    }
}