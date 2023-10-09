namespace N5.Eda.RequestReply.Interface.Model;

/// <summary>
/// Process awating to consume diferents topics until return informationn
/// </summary>
public interface IProcessAwait
{
    /// <summary>
    /// Session Id refer to process
    /// </summary>
    string IdSession { get; set; }

    /// <summary>
    /// Topic for start process
    /// </summary>
    string StartTopic { get; set; }

    /// <summary>
    /// Topic finish time
    /// </summary>
    string EndTopic { get; set; }

    /// <summary>
    /// Payload with Data
    /// </summary>
    object Payload { get; set; }
}