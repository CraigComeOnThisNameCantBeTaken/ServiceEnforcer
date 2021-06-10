using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PortsAndAdapters.Installation
{
    public static class PortsAndAdaptersInstaller
    {
        public static void EnforcePortsAndAdapters(this IServiceCollection services,
            Action<PortsAndAdaptersBuilder> builderAction)
        {
            var builder = new PortsAndAdaptersBuilder();
            builderAction(builder);

            services.AddTransient<IStartupFilter>(_ =>
            {
                return new PortsAvailableStartupFilter(services, builder.Ports);
            });
        }

    }
}
