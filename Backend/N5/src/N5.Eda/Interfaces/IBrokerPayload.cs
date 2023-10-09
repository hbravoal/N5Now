namespace N5.Eda.Interfaces;

/// <summary>
/// Payload message
/// </summary>
public interface IBrokerPayload
{
    /// <summary>
    /// Payload Message
    /// </summary>
    string Value { get; set; }

    /// <summary>
    /// Topic Name
    /// </summary>
    string Topic { get; set; }
}