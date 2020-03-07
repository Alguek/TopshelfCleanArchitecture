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

        public ReturnListOfProdutsCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ReturnListOfProdutsResponse> Handle(ReturnListOfProdutsCommand request, CancellationToken cancellationToken)
        {
            var listOfProduts = await _productRepository.ObterLista();
            return new ReturnListOfProdutsResponse(){ Id = 1 };
        }
    }
}
