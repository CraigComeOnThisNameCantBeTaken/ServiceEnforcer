using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServicesEnforcer
{
    public class PortInfo
    {
        public Type Port { get; set; }

        public ServiceLifetime LifeTime { get; set; }
    }
}
