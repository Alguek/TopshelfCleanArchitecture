using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces.Repository;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts
{
    public class ReturnListOfProdutsUseCase : IRequestHandler<ReturnListOfProdutsRequest, ReturnListOfProdutsResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ReturnListOfProdutsUseCase(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ReturnListOfProdutsResponse> Handle(ReturnListOfProdutsRequest request, CancellationToken cancellationToken)
        {
            var listOfProduts = await _productRepository.ObterLista();
            var response = _mapper.Map<ReturnListOfProdutsResponse>(listOfProduts);
            return response;
        }
    }
}