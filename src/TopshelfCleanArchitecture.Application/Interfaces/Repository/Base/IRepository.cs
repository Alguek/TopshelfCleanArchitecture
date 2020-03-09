using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Domain.Entities.Base;

namespace TopshelfCleanArchitecture.Application.Interfaces.Repository.Base
{
    public interface IRepository<TDomainModel> where TDomainModel : DomainModel
    {
        Task<TDomainModel> Inserir(TDomainModel obj);
        Task<TDomainModel> Alterar(TDomainModel obj);
        Task Excluir(int id);
        Task<TDomainModel> ObterPorId(int id);
        Task<IEnumerable<TDomainModel>> ObterLista();
    }
}
