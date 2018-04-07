using System;
using System.Threading;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.Commands
{
    public abstract class CommandHandler<TRequest, TResponse> : ICommandHandler
        where TResponse : IResponseModel
        where TRequest : IRequestModel
    {
        public Type ExpectedRequestType => typeof(TRequest);
        public Type ExpectedResponseType => typeof(TResponse);

        public async Task<IResponseModel> ExecuteInternalAsync(IRequestModel requestModel,
            CancellationToken cancellationToken)
        {
            return await ExecuteAsync((TRequest) requestModel, cancellationToken);
        }

        public abstract Task<TResponse> ExecuteAsync(TRequest requestModel, CancellationToken cancellationToken);
    }
}