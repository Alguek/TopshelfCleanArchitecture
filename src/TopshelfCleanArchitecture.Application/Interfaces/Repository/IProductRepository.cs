using System;
using System.Collections.Generic;
using System.Text;
using TopshelfCleanArchitecture.Domain.Entities;

namespace TopshelfCleanArchitecture.Application.Interfaces.Repository
{
    public interface IProductRepository : IRepository<ProductModel>
    {
    }
}
