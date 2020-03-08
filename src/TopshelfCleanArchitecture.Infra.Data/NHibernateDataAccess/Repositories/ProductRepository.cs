using AutoMapper;
using NHibernate;
using TopshelfCleanArchitecture.Application.Interfaces.Repository;
using TopshelfCleanArchitecture.Domain.Entities;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.Repositories.Base;

namespace TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.Repositories
{
    public class ProductRepository : Repository<ProductModel, ProductData>, IProductRepository
    {
        public ProductRepository(ISessionFactory sessionFactory, IMapper mapper) : base(sessionFactory, mapper)
        {

        }
    }
}
