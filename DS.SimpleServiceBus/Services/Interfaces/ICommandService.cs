using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.Services.Interfaces
{
    /// <summary>
    ///     Should only be implemented by CommandService. Inherit abstract class CommandService to build a new CommandService.
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        ///     Adds a CommandHandler
        /// </summary>
        /// <typeparam name="T">Type implementing ICommandHandker</typeparam>
        void RegisterCommandHandler<T>() where T : ICommandHandler;

        /// <summary>
        ///     Adds a CommandHandler
        /// </summary>
        /// <param name="commandHandler">Instance of class implementing CommandHandler</param>
        void RegisterCommandHandler(ICommandHandler commandHandler);

        /// <summary>
        ///     Sends a request through the IBusService and returns the response
        /// </summary>
        /// <typeparam name="TRequest">Type implementing IRequestModel</typeparam>
        /// <typeparam name="TResponse">Type implementing IResponseModel</typeparam>
        /// <param name="requestModel">Instance of class implementing IRequestModel</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        Task<TResponse> SendRequestAsync<TRequest, TResponse>(TRequest requestModel,
            CancellationToken cancellationToken) where TRequest : IRequestModel where TResponse : IResponseModel;

        /// <summary>
        ///     Disconnects CommandService from IBusService
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken);
    }
}