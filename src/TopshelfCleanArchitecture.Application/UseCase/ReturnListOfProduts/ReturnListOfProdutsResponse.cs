using System.Collections.Generic;
using TopshelfCleanArchitecture.Application.UseCase.Base;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts
{
    public class ReturnListOfProdutsResponse : ResponseBase
    {
        public List<ReturnListOfProdutsResponseProduct> Produts { get; set; }
    }
}