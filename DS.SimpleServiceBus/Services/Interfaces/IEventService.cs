using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Events.Interfaces;

namespace DS.SimpleServiceBus.Services.Interfaces
{
    public interface IEventService
    {
        Task PublishAsync(IEvent @event, CancellationToken cancellationToken);
        void RegisterEventHandler<T>() where T : IEventHandler;
        void RegisterEventHandler(IEventHandler eventHandler);
        Task EventMessageReceived(IEventMessage eventMessage, CancellationToken cancellationToken);
    }
}