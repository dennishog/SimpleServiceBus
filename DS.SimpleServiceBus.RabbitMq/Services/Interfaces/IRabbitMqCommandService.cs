using DS.SimpleServiceBus.Commands.Interfaces;
using MassTransit;

namespace DS.SimpleServiceBus.RabbitMq.Services.Interfaces
{
    internal interface IRabbitMqCommandService : IConsumer<ICommandMessage>
    {
    }
}