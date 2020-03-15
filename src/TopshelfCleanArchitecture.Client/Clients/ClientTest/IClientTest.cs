using System.Threading.Tasks;

namespace TopshelfCleanArchitecture.Client.Clients.ClientTest
{
    public interface IClientTest
    {
        Task<bool> GetComments();
    }
}
