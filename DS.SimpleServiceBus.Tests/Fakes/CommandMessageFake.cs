using DS.SimpleServiceBus.Commands;
using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Extensions;

namespace DS.SimpleServiceBus.Tests.Fakes
{
    public class CommandMessageFake : CommandMessage
    {
        public static CommandMessageFake GetCommandMessageFake(IRequestModel requestModel, IResponseModel responseModel)
        {
            return new CommandMessageFake
            {
                RequestData = requestModel.ToBytes(),
                ResponseData = responseModel.ToBytes()
            };
        }
    }
}