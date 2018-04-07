using DS.SimpleServiceBus.Configuration.Interfaces;
using System;

namespace DS.SimpleServiceBus.Configuration
{
    public class EventServiceConfigurator
    {
        public static IEventServiceConfiguration Configure(Action<IEventServiceConfiguration> bsc)
        {
            IEventServiceConfiguration config = new EventServiceConfiguration();
            bsc(config);
            return config;
        }
    }
}
