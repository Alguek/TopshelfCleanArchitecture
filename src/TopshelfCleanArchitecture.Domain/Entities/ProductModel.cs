using System;
using System.Collections.Generic;
using System.Text;
using TopshelfCleanArchitecture.Domain.Entities.Base;

namespace TopshelfCleanArchitecture.Domain.Entities
{
    public class ProductModel : DomainModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }
    }
}
