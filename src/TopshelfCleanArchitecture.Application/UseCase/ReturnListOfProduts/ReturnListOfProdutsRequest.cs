using MediatR;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts
{
    public class ReturnListOfProdutsRequest : IRequest<ReturnListOfProdutsResponse>
    {
        public int Id { get; set; }
    }
}