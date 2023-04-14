using PERM.Models.EmployeeMasterData;
using PERM_TASKS.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERM.Models.Attendance
{
    [Table("MarkAttendance")]

    public class EmployeeMarkAttendanceModel : ModelBase
    {
        //Mark Attendance
        [Key]
        [Required]
        [DisplayName("ID")]
        public int ID { get; set; }

        [Required]
        [DisplayName("Employee")]
        public string EmployeeID { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Status")] 
        public string Status { get; set; }

        [Required]
        [DisplayName("Shift")]
        public string Shift { get; set; }

        
        [DisplayName("Late Entry")]
        public bool lateEntry { get; set; }

        
        [DisplayName("EarlyExit")]
        public bool EarlyExit { get; set; }

        public EmployeeMasterDataModel Employee { get; set; }
    }
}
