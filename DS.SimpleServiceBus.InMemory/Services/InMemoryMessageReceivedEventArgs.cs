using DS.SimpleServiceBus.Events.Interfaces;
using System;

namespace DS.SimpleServiceBus.InMemory.Services
{
    public class InMemoryMessageReceivedEventArgs : EventArgs
    {
        public string QueueName { get; set; }
        public IEventMessage Message { get; set; }
    }
}
