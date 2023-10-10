

namespace N5.User.Domain.Entities
{
    public class UserPermission : Entity<int>
    {
      
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
}