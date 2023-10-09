namespace N5.Eda.Interfaces;

/// <summary>
/// Interface Broker Hadler List
/// </summary>
public interface IBrokerHandlerList
{
    /// <summary>
    /// Add eda Event
    /// </summary>
    /// <param name="kafkaEdaType">Type Eda</param>
    void Add(Type EdaType);

    /// <summary>
    /// Add Type specific for execute handler event
    /// </summary>
    /// <param name="kafkaEdaType">Type Services</param>
    /// <param name="topics">Array topics</param>
    void Add(Type kafkaEdaType, string[] topics);

    /// <summary>
    /// Return list events
    /// </summary>
    /// <returns></returns>
    Dictionary<string, List<Type>> GetEvents();
}