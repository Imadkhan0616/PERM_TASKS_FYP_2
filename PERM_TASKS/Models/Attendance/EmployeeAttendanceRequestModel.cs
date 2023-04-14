using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PERM.Models.Department;
using PERM.Models.EmployeeMasterData;
using PERM_TASKS.Models.Common;

namespace PERM.Models.Attendance
{

    [Table("AttendanceRequest")]
    public class EmployeeAttendanceRequestModel : ModelBase
    {
        //Attendance Request
        [Key]
        [Required]
        [DisplayName("ID")]
        public string ID { get; set; }

        [Required]
        [DisplayName("Employee ID")]
        public string EmployeeID { get; set; }

        [Required]
        [DisplayName("Department ID")]
        public string DepartmentID { get; set; }

        [Required]
        [DisplayName("From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        [DisplayName("To Date")]
        public DateTime ToDate { get; set; }

        [Required]
        [DisplayName("Half Day Date")]
        public DateTime HalfDayDate { get; set; }

        [Required]
        [DisplayName("Half Day")]
        public string HalfDay { get; set; }

        [Required]
        [DisplayName("Reason")]
        public string Reason { get; set; }

        public EmployeeMasterDataModel Employee { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
