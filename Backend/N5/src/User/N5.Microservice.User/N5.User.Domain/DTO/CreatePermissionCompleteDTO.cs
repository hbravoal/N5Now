using N5.Domain.Interfaces.DTO;

namespace N5.User.Domain.DTO;
public record CreatePermissionCompleteDTO : IPayloadMessage, IErrorManageMessage
{
    /// <summary>
    /// Permission id Created
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Created Date
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Error Message
    /// </summary>
    public string Error { get; set; }
    /// <summary>
    /// Error Code
    /// </summary>
    public int ErrorCode { get; set; }
    /// <summary>
    /// Session Id
    /// </summary>
    public string IdSession { get; set; }
}