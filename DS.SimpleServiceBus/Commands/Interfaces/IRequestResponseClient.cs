using System.Threading;
using System.Threading.Tasks;

namespace DS.SimpleServiceBus.Commands.Interfaces
{
    public interface IRequestResponseClient
    {
        Task<ICommandMessage> Request(ICommandMessage request,
            CancellationToken cancellationToken = new CancellationToken());
    }
}