
namespace N5.Eda.Interfaces;

/// <summary>
/// interface for generate event service
/// </summary>
public interface IBrokerEvent
{
    /// <summary>
    /// List topics apply to this class
    /// </summary>
    string[] Topic { get; }

    /// <summary>
    /// When publish event this method execute through server
    /// </summary>
    /// <param name="result">Result server</param>
    /// <param name="token">cancel token</param>
    /// <returns>Executed Task</returns>
    Task Handler(IBrokerPayload result, CancellationToken token);
}