
namespace N5.Domain.Interfaces.DTO;

/// <summary>
/// Interace for payload message (EndPont).
/// </summary>
public interface IPayloadMessage
{
    /// <summary>
    /// Session identifier for user
    /// </summary>
    public string IdSession { get; set; }
    /// <summary>
    /// Determine what operation will be handler
    /// </summary>
    public string NameOperation { get; set; }
}