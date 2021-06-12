using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ServicesEnforcer.Installation
{
    public class ServiceEnforcerBuilder
    {
        internal IList<ServiceInfo> Ports = new List<ServiceInfo>();

        public void EnforceSingleton<T>()
            where T : class
        {
            Enforce<T>(ServiceLifetime.Singleton);
        }

        public void EnforceScoped<T>()
            where T : class
        {
            Enforce<T>(ServiceLifetime.Transient);
        }

        public void EnforceTransient<T>()
            where T : class
        {
            Enforce<T>(ServiceLifetime.Transient);
        }

        private void Enforce<T>(ServiceLifetime? lifeTime = null)
            where T : class
        {
            Ports.Add(new ServiceInfo
            {
                Service = typeof(T),
                LifeTime = lifeTime
            });
        }
    }
}
