using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PortsAndAdapters.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortsAndAdapters
{
    internal class PortsAvailableStartupFilter : IStartupFilter
    {
        private readonly IServiceCollection _services;
        private readonly IEnumerable<PortInfo> _portInfos;

        public PortsAvailableStartupFilter(IServiceCollection services, IEnumerable<PortInfo> ports)
        {
            _services = services;
            _portInfos = ports;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                ValidatePorts(_services);
                next(builder);
            };
        }

        private void ValidatePorts(IServiceCollection services)
        {
            foreach (var portInfo in _portInfos)
            {
                var port = portInfo.Port;
                var lifeTime = portInfo.LifeTime;

                var service = services.FirstOrDefault(sd => sd.ServiceType == portInfo.Port &&
                    sd.Lifetime == portInfo.LifeTime);

                if (service == null)
                    throw new PortNotAdaptedException($"{port.Name} has not been adapted as a {lifeTime} service");
            }
        }
    }
}
