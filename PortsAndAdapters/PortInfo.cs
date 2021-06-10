using Microsoft.Extensions.DependencyInjection;
using System;

namespace PortsAndAdapters
{
    public class PortInfo
    {
        public Type Port { get; set; }

        public ServiceLifetime LifeTime { get; set; }
    }
}
