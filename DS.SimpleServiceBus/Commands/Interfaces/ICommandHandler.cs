using System;
using System.Threading;
using System.Threading.Tasks;

namespace DS.SimpleServiceBus.Commands.Interfaces
{
    public interface ICommandHandler
    {
        Type ExpectedRequestType { get; }
        Type ExpectedResponseType { get; }

        Task<IResponseModel> ExecuteInternalAsync(IRequestModel requestModel, CancellationToken cancellationToken);
    }
}