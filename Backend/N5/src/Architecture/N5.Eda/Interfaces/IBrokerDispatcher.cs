namespace N5.Eda.Interfaces;

/// <summary>
/// Dispacher Broker for suscribe event
/// </summary>
public interface IBrokerDispatcher
{

    /// <summary>
    /// Suscribe topic to broker
    /// </summary>
    /// <returns>Task action</returns>
    Task Subscribe();

    /// <summary>
    /// Validate if topic exist otherwise create
    /// </summary>
    /// <returns></returns>
    Task CreateTopic(string topic);
}