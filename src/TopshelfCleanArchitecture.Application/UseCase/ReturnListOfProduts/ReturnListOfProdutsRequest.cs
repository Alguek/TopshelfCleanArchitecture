using MediatR;
using TopshelfCleanArchitecture.Application.UseCase.Base;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts
{
    public class ReturnListOfProdutsRequest : IRequest<ResponseBase>
    {
        public int Id { get; set; }
    }
}