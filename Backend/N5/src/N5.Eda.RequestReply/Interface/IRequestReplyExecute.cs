
using N5.Domain.Interfaces.DTO;

namespace N5.Eda.RequestReply.Interface;

/// <summary>
/// State Machine
/// </summary>
public interface IRequestReplyExecute
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="payload"></param>
    /// <param name="startTopic"></param>
    /// <param name="endTopic"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<T> Wait<T>(IPayloadMessage payload, string startTopic, string endTopic, TimeSpan? timeout) where T : class, IPayloadMessage;
}