using Microsoft.Extensions.DependencyInjection;
using N5.Eda.Attributes;
using N5.Eda.Interfaces;
using System.Reflection;

namespace N5.Eda.Internals;

/// <summary>
/// List Class apply services have a attribute KafkaTopicAttribute
/// </summary>
/// <example>
/// [KafkaTopicAttribute(AvailabilityEvent.AVAILABILITY_SERVICE)]
/// </example>
internal class BrokerHandlerList : IBrokerHandlerList
{
    /// <summary>
    /// internal list event kafka
    /// </summary>
    private readonly Dictionary<string, List<Type>> _kafkaEvent = new();

    /// <summary>
    /// constructor list handler
    /// </summary>
    /// <param name="services">service .net Dependecy Injection</param>
    /// <param name="assemblies">List Assemblies for find service Events</param>
    /// <exception cref="Exception">List Assemby is null or empty</exception>
    public BrokerHandlerList(IServiceCollection services, params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies);
        if (assemblies.Length == 0)
            throw new Exception($"Assembly array is empty");

        var type = typeof(IBrokerEvent);
        List<string> assemblyName = new();

        assemblyName.Add(assemblies[0].FullName);
        assemblyName.AddRange(assemblies[0].GetReferencedAssemblies().Select(a => a.FullName));

        foreach (var name in assemblyName)
            foreach (var itemAssembly in Assembly.Load(name).GetTypes().Where(a => type.IsAssignableFrom(a) && a.IsClass))
            {
                Add(itemAssembly);
                services.AddTransient(itemAssembly);
            }
    }

    /// <summary>
    /// Add Type specific for execute handler event
    /// </summary>
    /// <param name="kafkaEdaType"></param>
    public void Add(Type kafkaEdaType)
    {
        var topicsApply = NameGetter.For(kafkaEdaType);
        if (topicsApply is null || topicsApply.Length == 0)
            return;

        foreach (var topic in topicsApply)
        {
            if (!_kafkaEvent.ContainsKey(topic))
                _kafkaEvent.Add(topic, new());

            _kafkaEvent[topic].Add(kafkaEdaType);
        }
    }

    /// <summary>
    /// Add Type specific for execute handler event
    /// </summary>
    /// <param name="kafkaEdaType">Type Services</param>
    /// <param name="topics">Array topics</param>
    public void Add(Type kafkaEdaType, string[] topics)
    {
        foreach (var topic in topics)
        {
            if (!_kafkaEvent.ContainsKey(topic))
                _kafkaEvent.Add(topic, new());

            _kafkaEvent[topic].Add(kafkaEdaType);
        }
    }

    /// <summary>
    /// Get list class with topics
    /// </summary>
    /// <returns>List topic whit all class apply event</returns>
    public Dictionary<string, List<Type>> GetEvents()
     => _kafkaEvent;
}