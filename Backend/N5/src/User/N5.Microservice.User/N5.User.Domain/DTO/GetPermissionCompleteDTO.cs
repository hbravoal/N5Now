using N5.Domain.Interfaces.DTO;

namespace N5.User.Domain.DTO;
public record GetPermissionCompleteDTO : IPayloadMessage, IErrorManageMessage
{

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

    #region Properties 
   public List<PermissionDto> Permissions { get; set; }
    #endregion
}