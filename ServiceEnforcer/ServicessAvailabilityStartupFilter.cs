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
        private readonly IEnumerable<ServiceInfo> _serviceInfos;

        public ServicesAvailableStartupFilter(IServiceCollection services, IEnumerable<ServiceInfo> serviceInfos)
        {
            _services = services;
            _serviceInfos = serviceInfos;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                ValidateServices(_services);
                next(builder);
            };
        }

        private void ValidateServices(IServiceCollection services)
        {
            foreach (var serviceInfo in _serviceInfos)
            {
                var service = services.FirstOrDefault(regSer => ServiceIsValid(regSer, serviceInfo));

                var expectedService = serviceInfo.Service;
                var expectedLifeTime = serviceInfo.LifeTime;
                if (service == null)
                    throw new ServiceNotEnforcedException($"{expectedService.Name} has not been registered as a {expectedLifeTime} service");
            }
        }

        private bool ServiceIsValid(ServiceDescriptor service, ServiceInfo serviceInfo)
{
            var expectedService = serviceInfo.Service;
            var expectedLifeTime = serviceInfo.LifeTime;

            return service.ServiceType == expectedService &&
                    (expectedLifeTime == null || service.Lifetime == expectedLifeTime);
        }
    }
}
