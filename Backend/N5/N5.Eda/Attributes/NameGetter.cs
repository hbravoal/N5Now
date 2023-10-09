namespace N5.Eda.Attributes;

/// <summary>
/// Static class for validate KafkaTopicAttribute
/// </summary>
public static class NameGetter
{
    /// <summary>
    /// Validate if type have attribure KafkaTopicAttribute
    /// </summary>
    /// <typeparam name="T">class for validate attribute</typeparam>
    /// <returns>List to Kafka topic attribute found in the type</returns>
    public static string[] For<T>()
    {
        return For(typeof(T));
    }

    /// <summary>
    /// Validate if type have attribure KafkaTopicAttribute
    /// </summary>
    /// <param name="type">type for validate attribute</param>
    /// <returns>List to Kafka topic attribute found in the type</returns>
    public static string[] For(Type type)
    {
        // add error checking ...
        return type.GetCustomAttributes(typeof(BrokerTopicHandler), false).Select(z => ((BrokerTopicHandler)z).Topic).ToArray();
    }
}