using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5.Telemetry.Discovery;

namespace N5.Telemetry;

public static class ServiceDiscovery
{
    public static void AddServiceDiscovery(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDiscovery(configuration); 
    }
}