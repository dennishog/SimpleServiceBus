using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.SimpleServiceBus.Configuration.Interfaces;

namespace DS.SimpleServiceBus.Configuration
{
    public class CommandServiceConfigurator
    {
        public static ICommandServiceConfiguration Configure(Action<ICommandServiceConfiguration> bsc)
        {
            ICommandServiceConfiguration config = new CommandServiceConfiguration();
            bsc(config);
            return config;
        }
    }
}
