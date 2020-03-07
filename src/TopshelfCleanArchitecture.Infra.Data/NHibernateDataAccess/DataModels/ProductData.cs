using System;
using System.Collections.Generic;
using System.Text;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels.Base;

namespace TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess.DataModels
{
    public class ProductData : DataModel
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual float Price { get; set; }
    }
}
