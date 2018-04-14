using DS.SimpleServiceBus.Commands.Interfaces;
using MassTransit;

namespace DS.SimpleServiceBus.RabbitMq.Services.Interfaces
{
    public interface IRabbitMqCommandService : SimpleServiceBus.Services.Interfaces.ICommandService, IConsumer<ICommandMessage>
    {
    }
}