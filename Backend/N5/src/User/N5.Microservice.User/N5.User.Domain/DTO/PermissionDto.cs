namespace N5.User.Domain.DTO;
    public class PermissionDto 
    { 
        /// <summary>
       ///  Id
       /// </summary>
        public int Id{ get; set; }

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