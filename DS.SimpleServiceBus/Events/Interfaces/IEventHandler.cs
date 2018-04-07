using System;
using System.Threading;
using System.Threading.Tasks;

namespace DS.SimpleServiceBus.Events.Interfaces
{
    public interface IEventHandler
    {
        Guid ForEvent { get; }
        Task ExecuteInternalAsync(IEvent @event, CancellationToken cancellationToken);
    }
}