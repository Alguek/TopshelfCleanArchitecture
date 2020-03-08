using FluentNHibernate.Mapping;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels;

namespace TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.Mappings
{
    public class ProductMap : ClassMap<ProductData>
    {
        public ProductMap()
        {
            Table("Product");
            Id(i => i.Id, "Id");
            Map(s => s.Name, "Name");
            Map(s => s.Price, "Price");
        }
    }
}
