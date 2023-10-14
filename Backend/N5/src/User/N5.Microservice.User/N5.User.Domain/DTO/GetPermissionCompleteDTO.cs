using N5.Domain.Interfaces.DTO;

namespace N5.User.Domain.DTO;
public record GetPermissionCompleteDTO : IPayloadMessage, IErrorManageMessage
{

    /// <summary>
    /// User's Session
    /// </summary>
    public string IdSession { get; set; }
    /// <summary>
    /// Determine what is the current operation
    /// </summary>
    public string NameOperation { get => "get"; }

    /// <summary>
    /// Error Message
    /// </summary>
    public string Error { get; set; }
    /// <summary>
    /// Error Code
    /// </summary>
    public int ErrorCode { get; set; }
 
    #region Properties 
   public List<PermissionDto> Permissions { get; set; }
    #endregion
}