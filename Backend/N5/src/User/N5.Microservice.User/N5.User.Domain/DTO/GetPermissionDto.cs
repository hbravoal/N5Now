using N5.Domain.Interfaces.DTO;

namespace N5.User.Domain.DTO;
    public class GetPermissionDto : IPayloadMessage
{ 
    /// <summary>
   /// Session Id
   /// </summary>
    public string IdSession { get; set; }

    /// <summary>
    /// Page
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// PageSize
    /// </summary>
    public int PageSize { get; set; }

}