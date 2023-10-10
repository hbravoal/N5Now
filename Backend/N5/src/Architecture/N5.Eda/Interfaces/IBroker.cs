namespace N5.Eda.Interfaces;

/// <summary>
/// interface base for use Kafka
/// </summary>
public interface IBroker
{
    /// <summary>
    /// Send request from app to kafka's server
    /// </summary>
    /// <param name="topic">Name event for subscribe</param>
    /// <param name="payload">objet send to kafka</param>
    /// <returns></returns>
    Task Publish(string topic, object payload);

    /// <summary>
    /// build robots events await to execute event
    /// </summary>
    void Build();
}