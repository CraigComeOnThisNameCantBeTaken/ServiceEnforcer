using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ServicesEnforcer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServicesEnforcer
{
    internal class ServicesAvailableStartupFilter : IStartupFilter
    {
        private readonly IServiceCollection _services;
        private readonly IEnumerable<ServiceInfo> _portInfos;

        public ServicesAvailableStartupFilter(IServiceCollection services, IEnumerable<ServiceInfo> ports)
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
                var expectedService = portInfo.Service;
                var expectedLifeTime = portInfo.LifeTime;

                var service = services.FirstOrDefault(regSer => regSer.ServiceType == portInfo.Service &&
                    (expectedLifeTime == null || regSer.Lifetime == expectedLifeTime));

                if (service == null)
                    throw new ServiceNotEnforcedException($"{expectedService.Name} has not been registered as a {expectedLifeTime} service");
            }
        }
    }
}
