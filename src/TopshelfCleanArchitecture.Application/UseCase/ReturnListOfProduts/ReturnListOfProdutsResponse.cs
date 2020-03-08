using System;
using System.Collections.Generic;
using System.Text;
using TopshelfCleanArchitecture.Application.UseCase.Base;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts
{
    public class ReturnListOfProdutsResponse : ResponseBase
    {
        public List<ProductReturnListOfProdutsResponse> Produts { get; set; }
    }
}
