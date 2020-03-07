using System.Threading;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces.Job;

namespace TopshelfCleanArchitecture
{
    public class Startup
    {
        private readonly IQuartzHostedService _quartzHostedService;
        public Startup(IQuartzHostedService quartzHostedService)
        {
            _quartzHostedService = quartzHostedService;
        }

        public async Task StartAsync()
        {
            await _quartzHostedService.StartAsync(cancellationToken: CancellationToken.None);
        }

        public async Task StopAsync()
        {
            await _quartzHostedService.StopAsync(cancellationToken: CancellationToken.None);
        }
    }
}
