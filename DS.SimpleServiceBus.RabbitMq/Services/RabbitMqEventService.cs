using DS.SimpleServiceBus.RabbitMq.Configuration.Interfaces;
using DS.SimpleServiceBus.RabbitMq.Services.Interfaces;
using DS.SimpleServiceBus.Services;
using DS.SimpleServiceBus.Services.Interfaces;

namespace DS.SimpleServiceBus.RabbitMq.Services
{
    internal class RabbitMqEventService : EventService, IRabbitMqEventService
    {
        public RabbitMqEventService(IBusService busService, IRabbitMqEventServiceConfiguration config) : base(
            busService,
            config)
        {
        }
    }
}