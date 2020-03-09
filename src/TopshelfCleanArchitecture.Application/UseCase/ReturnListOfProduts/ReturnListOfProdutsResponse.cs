using System.Collections.Generic;
using TopshelfCleanArchitecture.Application.UseCase.Base;

namespace TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts
{
    public class ReturnListOfProdutsResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }
    }
}