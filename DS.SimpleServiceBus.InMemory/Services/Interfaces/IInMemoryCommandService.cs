using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.InMemory.Services.Interfaces
{
    public interface IInMemoryCommandService
    {
        Task<ICommandMessage> Consume(ICommandMessage message);
    }
}