
using N5.Eda.Interfaces;

namespace N5.Eda;

/// <summary>
/// Basic Broker Dispacher
/// </summary>
public abstract class BrokerDispatcher : IBrokerDispatcher
{
    /// <summary>
    /// Service provider Injection Dependency
    /// </summary>
    protected readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// List events
    /// </summary>
    protected readonly List<IBrokerEvent> _events;

    /// <summary>
    /// Constructor base
    /// </summary>
    /// <param name="serviceProvider">Service Dependency Injection</param>
    public BrokerDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _events = new();
    }



    /// <summary>
    /// Create List Handlers
    /// </summary>
    /// <param name="topic">Topic Name</param>
    /// <param name="eventTypes">List Event Handlers</param>
    ///
    /*
    public virtual void Build(string topic, List<Type> eventTypes)
    {
        foreach (var itemEvent in eventTypes)
        {
            var instance = _serviceProvider.GetService(itemEvent) as IBrokerEvent;
            ArgumentNullException.ThrowIfNull(instance);

            _events.Add(instance);
        }
    }
    */

    /// <summary>
    /// Create Topic
    /// </summary>
    /// <returns>Task Action</returns>
    public abstract Task CreateTopic(string topic);

    /// <summary>
    /// Suscribe event to broker
    /// </summary>
    /// <returns>Task Action</returns>
    public abstract Task Subscribe();
}