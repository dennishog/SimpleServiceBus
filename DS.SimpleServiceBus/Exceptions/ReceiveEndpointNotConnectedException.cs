﻿using System;

namespace DS.SimpleServiceBus.Exceptions
{
    public class ReceiveEndpointNotConnectedException : Exception
    {
        public ReceiveEndpointNotConnectedException()
        {
        }

        public ReceiveEndpointNotConnectedException(string message) : base(message)
        {
        }

        public ReceiveEndpointNotConnectedException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}