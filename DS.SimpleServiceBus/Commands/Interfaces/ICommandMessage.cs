using System;

namespace DS.SimpleServiceBus.Commands.Interfaces
{
    public interface ICommandMessage
    {
        byte[] RequestData { get; set; }
        byte[] ResponseData { get; set; }
        Type ExpectedRequestType { get; set; }
        Type ExpectedResponseType { get; set; }
    }
}