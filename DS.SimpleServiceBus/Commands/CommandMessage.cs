using System;
using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.Commands
{
    public class CommandMessage : ICommandMessage
    {
        public byte[] RequestData { get; set; }
        public byte[] ResponseData { get; set; }
        public Type ExpectedRequestType { get; set; }
        public Type ExpectedResponseType { get; set; }
    }
}