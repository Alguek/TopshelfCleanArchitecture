using System.Net.Http;
using System.Threading.Tasks;

namespace TopshelfCleanArchitecture.Client.Clients.ClientTest
{
    public class ClientTest : IClientTest
    {
        private readonly string relativeApi = "comments";

        private readonly HttpClient _httpClient;

        public ClientTest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> GetComments()
        {
            var request = await _httpClient.GetAsync(relativeApi + "teste");
            var response = await request.Content.ReadAsAsync<object>();
            return await Task.FromResult(true);
        }
    }
}
