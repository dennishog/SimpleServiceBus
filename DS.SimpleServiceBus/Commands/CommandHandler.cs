using System;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.Commands
{
    /// <summary>
    ///     Abstract class used for implementing a CommandHandler
    /// </summary>
    /// <typeparam name="TRequest">Type implementing IRequestModel</typeparam>
    /// <typeparam name="TResponse">Type implementing IResponseModel</typeparam>
    public abstract class CommandHandler<TRequest, TResponse> : ICommandHandler
        where TResponse : IResponseModel
        where TRequest : IRequestModel
    {
        /// <summary>
        ///     Used internally for finding the correct CommandHandler
        /// </summary>
        public Type ExpectedRequestType => typeof(TRequest);

        /// <summary>
        ///     Used internally for finding the correct CommandHandler
        /// </summary>
        public Type ExpectedResponseType => typeof(TResponse);

        /// <summary>
        ///     Execution of CommandHandlers are internally executed through this method
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResponseModel> ExecuteInternalAsync(IRequestModel requestModel,
            CancellationToken cancellationToken)
        {
            return await ExecuteAsync((TRequest) requestModel, cancellationToken);
        }

        /// <summary>
        ///     Implemented by CommandHandlers in order for the CommandHandlers to get a method with the correct types
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<TResponse> ExecuteAsync(TRequest requestModel, CancellationToken cancellationToken);
    }
}