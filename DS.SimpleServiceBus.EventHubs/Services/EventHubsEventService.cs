using DS.SimpleServiceBus.Configuration.Interfaces;
using DS.SimpleServiceBus.EventHubs.Services.Interfaces;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.EventHubs.Services
{
    public class EventHubsEventService : EventService, IEventHubsEventService
    {
        public EventHubsEventService(IBusService busService, IEventServiceConfiguration action) : base(busService,
            action)
        {
        }
    }
}