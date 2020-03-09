using AutoMapper;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces.Repository.Base;
using TopshelfCleanArchitecture.Domain.Entities.Base;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels.Base;

namespace TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.Repositories.Base
{
    public class Repository<TDomainModel, TDataModel> : IRepository<TDomainModel>
        where TDomainModel : DomainModel
        where TDataModel : DataModel
    {
        protected readonly ISessionFactory _sessionFactory;
        protected readonly IMapper _mapper;

        public Repository(ISessionFactory sessionFactory, IMapper mapper)
        {
            _sessionFactory = sessionFactory;
            _mapper = mapper;
        }

        public async Task<TDomainModel> Inserir(TDomainModel domainModel)
        {
            var dataModel = _mapper.Map<TDataModel>(domainModel);

            using var session = _sessionFactory.OpenSession();
            await session.SaveAsync(dataModel);
            await session.FlushAsync();
            return domainModel;
        }

        public async Task<TDomainModel> Alterar(TDomainModel domainModel)
        {
            var dataModel = _mapper.Map<TDataModel>(domainModel);

            using var session = _sessionFactory.OpenSession();
            await session.UpdateAsync(dataModel);
            await session.FlushAsync();
            return domainModel;
        }

        public async Task Excluir(int id)
        {
            using var session = _sessionFactory.OpenSession();
            await session.DeleteAsync(await session.LoadAsync<TDataModel>(id));
            await session.FlushAsync();
        }

        public async Task<TDomainModel> ObterPorId(int id)
        {
            using var session = _sessionFactory.OpenSession();
            var dataModel = await session.GetAsync<TDataModel>(id);
            var domainModel = _mapper.Map<TDomainModel>(dataModel);
            return domainModel;
        }

        public async Task<IEnumerable<TDomainModel>> Obter(Expression<Func<TDataModel, bool>> predicate)
        {
            using var session = _sessionFactory.OpenSession();
            var dataModel = await session.Query<TDataModel>().Where(predicate).ToListAsync();
            var domainModel = _mapper.Map<IEnumerable<TDomainModel>>(dataModel);
            return domainModel;
        }

        public async Task<IEnumerable<TDomainModel>> ObterLista()
        {
            using var session = _sessionFactory.OpenSession();
            var lista = await session.Query<TDataModel>().ToListAsync();
            return _mapper.Map<IEnumerable<TDomainModel>>(lista);
        }
    }
}
