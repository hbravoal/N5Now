using N5.Domain.Interfaces.DTO;

namespace N5.User.Domain.DTO;
    public class GetPermissionDto : IPayloadMessage
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
    /// Page
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// PageSize
    /// </summary>
    public int PageSize { get; set; }

}