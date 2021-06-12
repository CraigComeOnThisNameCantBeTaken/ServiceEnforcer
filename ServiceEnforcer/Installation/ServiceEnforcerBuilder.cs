using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ServicesEnforcer.Installation
{
    public class ServiceEnforcerBuilder
    {
        public IList<PortInfo> Ports = new List<PortInfo>();

        public void AddPort<T>(ServiceLifetime lifeTime)
            where T : class
        {
            Ports.Add(new PortInfo
            {
                Port = typeof(T),
                LifeTime = lifeTime
            });
        }
    }
}
