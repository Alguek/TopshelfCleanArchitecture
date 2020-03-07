using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels;

namespace TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.Mappings
{
    public class ProductMap : ClassMap<ProductData>
    {
        public ProductMap()
        {
            Table("Product");
            Id(i => i.Id, "Id");
        }
    }
}
