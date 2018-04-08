using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Events;

namespace DS.SimpleServiceBus.Tests.Fakes
{
    public class EventHandlerFake : EventHandler<EventFake>
    {
        public override async Task ExecuteAsync(EventFake @event, CancellationToken cancellationToken)
        {
            await Task.Run(() => { Debug.WriteLine($"Recieved id {@event.Model.Id} and name {@event.Model.Name}"); },
                cancellationToken);
        }
    }
}