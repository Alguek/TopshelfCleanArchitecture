﻿using AutoMapper;
using System.Collections.Generic;
using TopshelfCleanArchitecture.Application.UseCase.ReturnListOfProduts;
using TopshelfCleanArchitecture.Domain.Entities;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.AutoMapper
{
    public class ReturnListOfProdutsProfile : Profile
    {
        public ReturnListOfProdutsProfile()
        {
            CreateMap<ProductModel, ReturnListOfProdutsResponse>();

            //CreateMap<List<ProductModel>, ReturnListOfProdutsResponse>()
            //    .ForMember(s => s.Produts, m => m.MapFrom(x => x));
        }
    }
}
