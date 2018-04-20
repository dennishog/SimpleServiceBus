using System;
using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.Commands
{
    /// <summary>
    ///     Internal class used for sending request/response through the broker
    /// </summary>
    public class CommandMessage : ICommandMessage
    {
        /// <summary>
        ///     The serialized IRequestModel
        /// </summary>
        public byte[] RequestData { get; set; }

        /// <summary>
        ///     The serialized IResponseModel
        /// </summary>
        public byte[] ResponseData { get; set; }

        /// <summary>
        ///     The type of the implementation of IRequestModel
        /// </summary>
        public Type ExpectedRequestType { get; set; }

        /// <summary>
        ///     The type of the implementation of IResponseModel
        /// </summary>
        public Type ExpectedResponseType { get; set; }
    }
}