using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServicesEnforcer
{
    internal class ServiceInfo
    {
        public Type Service { get; set; }

        public ServiceLifetime? LifeTime { get; set; }
    }
}
