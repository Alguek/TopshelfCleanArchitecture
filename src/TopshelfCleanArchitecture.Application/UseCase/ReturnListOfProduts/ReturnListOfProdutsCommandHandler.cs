using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces.Repository;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts
{
    public class ReturnListOfProdutsCommandHandler : IRequestHandler<ReturnListOfProdutsCommand, ReturnListOfProdutsResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ReturnListOfProdutsCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ReturnListOfProdutsResponse> Handle(ReturnListOfProdutsCommand request, CancellationToken cancellationToken)
        {
            var listOfProduts = await _productRepository.ObterLista();
            var response = _mapper.Map<ReturnListOfProdutsResponse>(listOfProduts);
            return response;
        }
    }
}
