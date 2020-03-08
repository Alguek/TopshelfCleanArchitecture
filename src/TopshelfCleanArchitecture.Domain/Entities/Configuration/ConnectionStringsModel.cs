using TopshelfCleanArchitecture.Domain.Enum;

namespace TopshelfCleanArchitecture.Domain.Models.Configuration
{
    public class ConnectionStringsModel
    {
        public string DefaultConnection { get; set; }
        public EDatabaseType DatabaseType { get; set; }
    }
}
