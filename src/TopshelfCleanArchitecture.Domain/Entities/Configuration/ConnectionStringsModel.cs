using System;
using System.Collections.Generic;
using System.Text;
using TopshelfCleanArchitecture.Domain.Enum;

namespace TopshelfCleanArchitecture.Domain.Models.Configuration
{
    public class ConnectionStringsModel
    {
        public string DefaultConnection { get; set; }
        public eDatabaseType DatabaseType { get; set; }
    }
}
