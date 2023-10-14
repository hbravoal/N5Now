using N5.Domain.Interfaces.DTO;

namespace N5.User.Domain.DTO;
    /// <summary>
    /// Modify User permission
    /// </summary>
    public class ModifyPermissionDto : IPayloadMessage
{
    /// <summary>
    /// User's Session
    /// </summary>
    public string IdSession { get; set; }
    /// <summary>
    /// Determine what is the current operation
    /// </summary>
    public string NameOperation { get => "modify"; }
    /// <summary>
    /// Session Id
    /// </summary>
    public int  Id{ get; set; }

    /// <summary>
    /// Employee name
    /// </summary>
    public string EmployeeForename { get; set; }

    /// <summary>
    /// Employee name
    /// </summary>
    public string EmployeeSurname { get; set; }

    /// <summary>
    /// Permission type Id Id
    /// </summary>
    public int PermissionTypeId { get; set; }

    /// <summary>
    /// Permission Date
    /// </summary>
    public DateTime PermissionDate { get; set; }



}