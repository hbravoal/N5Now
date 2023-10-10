namespace N5.Eda.Attributes;

/// <summary>
/// Attribute for add service kafka to list of subscribed to kafka
/// </summary>
/// <example>
/// [KafkaTopicAttribute(AvailabilityEvent.AVAILABILITY_SERVICE)]
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class BrokerTopicHandler : Attribute
{
    /// <summary>
    /// Name event
    /// </summary>
    public string Topic { get; private set; }

    /// <summary>
    /// Constructor base Kafka topic
    /// </summary>
    /// <param name="topic">Name event to subcribed</param>
    public BrokerTopicHandler(string topic)
    {
        Topic = topic;
    }
}