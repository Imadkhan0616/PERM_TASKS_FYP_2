using PERM.Models.Department;
using PERM_TASKS.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERM.Models.Attendance
{
    [Table("EmployeeAttendanceAssistant")]

    public class EmployeeAttendanceAssistantModel : ModelBase
    {
        //Employee Attendance Assistant
        [Key]
        [Required]
        [DisplayName("ID")]
        public int ID { get; set; }

        [Required]
        [DisplayName("Date")]
        public DateTime AT_Date { get; set; }

        [Required]
        [DisplayName("Branch")]
        public string AT_Branch { get; set; }

        [Required]
        [DisplayName("Department ID")]
        public string DepartmentID { get; set; }

        public DepartmentModel Department { get; set; }
    }
}
