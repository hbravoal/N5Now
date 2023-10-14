using N5.Domain.Interfaces.DTO;

namespace N5.User.Domain.DTO;
public record ModifyPermissionCompleteDTO : IPayloadMessage, IErrorManageMessage
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
    /// Permission id Created
    /// </summary>
    public int Id { get; set; }

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

}