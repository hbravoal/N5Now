using N5.Eda.Interfaces;

namespace N5.Eda;

public abstract class BrokerHandler
{
    /// <summary>
    /// Configuration kafka for connect
    /// </summary>
    protected readonly IBrokerConfiguration _configuration;

    /// <summary>
    /// Constructor base
    /// </summary>
    /// <param name="configuration">Configuration kafka to connect</param>
    public BrokerHandler(IBrokerConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// List topics
    /// </summary>
    public string[] Topic { get; set; }

    /// <summary>
    /// Handler to receive information about kafka's event
    /// </summary>
    /// <param name="result">information response kafka's event</param>
    /// <param name="token">Cancel token information</param>
    /// <returns>Task execution</returns>
    public abstract Task Handler(IBrokerPayload result, CancellationToken token);
}