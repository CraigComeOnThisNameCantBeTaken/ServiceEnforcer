using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServicesEnforcer.Installation
{
    public static class ServiceEnforcerInstaller
    {
        public static void EnforceServices(this IServiceCollection services,
            Action<ServiceEnforcerBuilder> builderAction)
        {
            var builder = new ServiceEnforcerBuilder();
            builderAction(builder);

            services.AddTransient<IStartupFilter>(_ =>
            {
                return new ServicesAvailableStartupFilter(services, builder.Ports);
            });
        }

    }
}
