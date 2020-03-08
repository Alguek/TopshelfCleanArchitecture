using System.Threading;
using System.Threading.Tasks;

namespace TopshelfCleanArchitecture.Application.Interfaces.Job
{
    public interface IQuartzHostedService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
