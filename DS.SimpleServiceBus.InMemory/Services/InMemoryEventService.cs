using DS.SimpleServiceBus.Configuration.Interfaces;
using DS.SimpleServiceBus.InMemory.Services.Interfaces;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Services
{
    public class InMemoryEventService : EventService, IInMemoryEventService
    {
        public InMemoryEventService(IBusService busService, IEventServiceConfiguration action) : base(busService,
            action)
        {
        }
    }
}