using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N5.Eda.Interfaces;
using N5.Eda.Internals;
using N5.Eda.Model;
using System.Reflection;

namespace N5.Eda.Extensions;

/// <summary>
/// Extension Eda
/// </summary>
public static class EdaExtension
{
    /// <summary>
    /// Add services to broker list, we recomend use Assembly.GetEntryAssembly())
    /// </summary>
    /// <param name="services">collection services with dependecy injection</param>
    /// <returns>collection services</returns>
    /// <example>
    /// services.UseMessageBroker(Assembly.GetEntryAssembly());
    /// </example>
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddSingleton<IBrokerHandlerList>(new BrokerHandlerList(services, assemblies));
        return services;
    }

    public static IHost UseMessageBroker(this IHost app, Action<EdaConfiguration> edaConfogiration = null)
    {
        var config = new EdaConfiguration(app.Services.GetRequiredService<IBrokerHandlerList>());
        if (edaConfogiration != null)
            edaConfogiration(config);

        return app;
    }
}