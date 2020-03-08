using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts;
using TopshelfCleanArchitecture.Domain.Entities;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.AutoMapper
{
    public class ReturnListOfProdutsProfile : Profile
    {
        public ReturnListOfProdutsProfile()
        {
            CreateMap<ProductModel,ProductReturnListOfProdutsResponse>();
                
            CreateMap<List<ProductModel>,ReturnListOfProdutsResponse>()
                .ForMember(s => s.Produts, m => m.MapFrom(x => x));
        }
    }
}
