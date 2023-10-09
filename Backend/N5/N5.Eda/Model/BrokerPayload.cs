using N5.Eda.Interfaces;

namespace N5.Eda.Model;

/// <summary>
/// Base Payload
/// </summary>
public class BrokerPayload : IBrokerPayload
{
    /// <summary>
    /// Payload Message
    /// </summary>
    public string Value { get; set; }

    /// <summary>S
    /// Topic Name
    /// </summary>
    public string Topic { get; set; }
}