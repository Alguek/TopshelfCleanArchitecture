using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TopshelfCleanArchitecture.Domain.Entities.Base;
using TopshelfCleanArchitecture.Domain.Entities.Configuration;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels.Base;

namespace TopshelfCleanArchitecture.Infra.CrossCutting.AutoMapper
{
    public class InfraProfile : Profile
    {
        public InfraProfile()
        {
            IDictionary<string, List<Type>> dictionaryTypes = new Dictionary<string, List<Type>>();

            var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes())
               .Where(p => (typeof(DataModel).IsAssignableFrom(p) || typeof(DomainModel).IsAssignableFrom(p)) && p.IsPublic && !p.IsAbstract)
               .Distinct()
               .ToList();

            foreach (var type in types)
            {
                string name = ReplaceLastOccurrence(ReplaceLastOccurrence(type.Name, "Model", ""), "Data", "");
                if (dictionaryTypes.ContainsKey(name))
                    dictionaryTypes[name].Add(type);
                else
                {
                    dictionaryTypes.Add(name, new List<Type>());
                    dictionaryTypes[name].Add(type);
                }
            }

            foreach (var dictionaryType in dictionaryTypes.Values)
            {
                CreateMap(dictionaryType[0], dictionaryType[1]);
                CreateMap(dictionaryType[1], dictionaryType[0]);
            }

            //var types = AppDomain.CurrentDomain.GetAssemblies()
            //   .SelectMany(s => s.GetTypes())
            //   .Where(p => (typeof(DataModel).IsAssignableFrom(p) || typeof(DomainModel).IsAssignableFrom(p)) && p.IsPublic && !p.IsAbstract)
            //   .Distinct()
            //   .ToList();

            //var dataModelList = types.Where(p => typeof(DataModel).IsAssignableFrom(p));
            //var domainModelList = types.Where(p => typeof(DomainModel).IsAssignableFrom(p));

            //foreach (var dataModel in dataModelList)
            //{
            //    var domainModel = domainModelList.FirstOrDefault(s => ReplaceLastOccurrence(s.Name, "Model", "") ==  ReplaceLastOccurrence(dataModel.Name, "Data", ""));
            //    if (domainModel == null)
            //        continue;

            //    CreateMap(domainModel, dataModel);
            //    CreateMap(dataModel, domainModel);
            //}
        }
        string ReplaceLastOccurrence(string str, string toReplace, string replacement)
        {
            return Regex.Replace(str, $@"^(.*){toReplace}(.*?)$", $"$1{replacement}$2");
        }
    }
}
