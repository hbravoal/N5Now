using N5.Domain.Interfaces.DTO;

namespace N5.User.Domain.DTO;
    public class CreatePermissionDto : IPayloadMessage
{
    /// <summary>
    /// User's Session
    /// </summary>
    public string IdSession { get; set; }
    /// <summary>
    /// Determine what is the current operation
    /// </summary>
    public string NameOperation { get => "request"; }

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