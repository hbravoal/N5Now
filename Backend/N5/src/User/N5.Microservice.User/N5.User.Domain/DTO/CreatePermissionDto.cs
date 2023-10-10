using N5.Domain.Interfaces.DTO;

namespace N5.User.Domain.DTO;
    public class CreatePermissionDto : IPayloadMessage
{ 
    /// <summary>
   /// Session Id
   /// </summary>
    public string IdSession { get; set; }

    /// <summary>
    /// Document name
    /// </summary>
    public string Name { get; set; }

   
    public string Title { get; set; }

    /// <summary>
    /// User Id
    /// </summary>
    public int UserId { get; set; }
}