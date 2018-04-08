using System.Threading;
using System.Threading.Tasks;

namespace DS.SimpleServiceBus.Events.Interfaces
{
    public interface IEventHandler
    {
        Task ExecuteInternalAsync(IEvent @event, CancellationToken cancellationToken);
    }
}