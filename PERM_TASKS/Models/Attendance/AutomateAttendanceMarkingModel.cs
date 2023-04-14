using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PERM_TASKS.Models.Common;

namespace PERM.Models.Attendance
{
    [Table("AutomateAttendanceMarking")]

    public class AutomateAttendanceMarkingModel : ModelBase
    {
        //Automate Attendance Marking
        [Key]
        [Required]
        [DisplayName("ID")]
        public int ID { get; set; }

        [Required]
        [DisplayName("Upload Attendance")]
        public string UploadAttendance { get; set; }

        [Required]
        [DisplayName("From Date")]
        public DateTime ATM_FromDate { get; set; }

        [Required]
        [DisplayName("To Date")]
        public DateTime ATM_ToDate { get; set; }
    }
}
