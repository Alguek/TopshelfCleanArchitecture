using AutoMapper;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using TopshelfCleanArchitecture.Domain.Entities.Base;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels.Base;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.AutoMapper
{
    public class InfraProfile : Profile
    {
        public InfraProfile()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes())
               .Where(p => (typeof(DataModel).IsAssignableFrom(p) || typeof(DomainModel).IsAssignableFrom(p)) && p.IsPublic && !p.IsAbstract)
               .Distinct()
               .ToList();

            var dataModelList = types.Where(p => typeof(DataModel).IsAssignableFrom(p));
            var domainModelList = types.Where(p => typeof(DomainModel).IsAssignableFrom(p));

            foreach (var dataModel in dataModelList)
            {
                var domainModel = domainModelList.FirstOrDefault(s => ReplaceLastOccurrence(s.Name, "Model", "") ==  ReplaceLastOccurrence(dataModel.Name, "Data", ""));
                if (domainModel == null)
                    continue;

                CreateMap(domainModel, dataModel);
                CreateMap(dataModel, domainModel);
            }
        }
        string ReplaceLastOccurrence(string str, string toReplace, string replacement)
        {
            return Regex.Replace(str, $@"^(.*){toReplace}(.*?)$", $"$1{replacement}$2");
        }
    }
}
