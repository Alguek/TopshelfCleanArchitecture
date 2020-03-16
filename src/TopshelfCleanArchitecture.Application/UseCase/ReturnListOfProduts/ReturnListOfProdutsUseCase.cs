using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces.Repository;
using TopshelfCleanArchitecture.Application.UseCase.Base;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts
{
    public class ReturnListOfProdutsUseCase : IRequestHandler<ReturnListOfProdutsRequest, ResultBase<List<ReturnListOfProdutsResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ReturnListOfProdutsUseCase(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultBase<List<ReturnListOfProdutsResponse>>> Handle(ReturnListOfProdutsRequest request, CancellationToken cancellationToken)
        {
            var listOfProduts = await _productRepository.ObterLista();
            var response = _mapper.Map<List<ReturnListOfProdutsResponse>>(listOfProduts);
            var result = new ResultBase<List<ReturnListOfProdutsResponse>>(response);
            return result;
        }
    }
}