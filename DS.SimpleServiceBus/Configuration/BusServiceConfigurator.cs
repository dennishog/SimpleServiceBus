using DS.SimpleServiceBus.Configuration.Interfaces;
using System;

namespace DS.SimpleServiceBus.Configuration
{
    public class BusServiceConfigurator
    {
        public static IBusServiceConfiguration Configure(Action<IBusServiceConfiguration> bsc)
        {
            IBusServiceConfiguration config = new BusServiceConfiguration();
            bsc(config);
            return config;
        }
    }
}
