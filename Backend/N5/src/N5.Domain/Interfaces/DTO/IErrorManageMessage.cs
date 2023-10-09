namespace N5.Domain.Interfaces.DTO;

/// <summary>
/// Interface for manage error in messages DTO
/// </summary>
public interface IErrorManageMessage
{
    /// <summary>
    /// Error Message
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// Error Code
    /// </summary>
    public int ErrorCode { get; set; }
}