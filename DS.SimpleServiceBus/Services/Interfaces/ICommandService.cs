using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;
using MassTransit;

namespace DS.SimpleServiceBus.Services.Interfaces
{
    public interface ICommandService : IConsumer<ICommandMessage>
    {
        void RegisterCommandHandler<T>() where T : ICommandHandler;
        void RegisterCommandHandler(ICommandHandler commandHandler);

        Task<TResponse> SendRequestAsync<TRequest, TResponse>(TRequest requestModel,
            CancellationToken cancellationToken) where TRequest : IRequestModel where TResponse : IResponseModel;

        Task StopAsync(CancellationToken cancellationToken);
    }
}