using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace PortsAndAdapters.Installation
{
    public class PortsAndAdaptersBuilder
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
