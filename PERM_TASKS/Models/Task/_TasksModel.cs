using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using PERM.Models.EmployeeMasterData;
using PERM_TASKS.Models.Common;

namespace PERM.Models.Task
{
    [Table("Tasks")]

    public class _TasksModel : ModelBase
    {
        [Key]
        [DisplayName("ID")]
        public int TaskID { get; set; }

        [Required]
        [DisplayName("Name")]
        public string TaskName { get; set; }

        [Required]
        [DisplayName("Description")]
        public string TaskDescription { get; set; }

        [Required]
        [DisplayName("Status")]
        public bool TaskStatus { get; set; }

        [Required]
        [DisplayName(" _Tasks Type")]
        public string TaskType { get; set; }

        [Required]
        [DisplayName("Priority")]
        public string Priority { get; set; }

        [Required]
        [DisplayName("Created Date")]
        public DateTime TaskCreatedDate { get; set; }

        [Required]
        [DisplayName("Updated Date")]
        public DateTime TaskUpdatedDate { get; set; }

        [Required]
        [DisplayName("DeadLine")]
        public DateTime TaskDeadline { get; set; }


        [DisplayName("Completion Date")]
        public DateTime TaskCompletionDate { get; set; }

        [Required]
        [DisplayName("Assigned To")]
        public string TaskAssignedToID { get; set; }

        [Required]
        [DisplayName("Assigned By")]
        public string TaskAssignedByID { get; set; }

        public EmployeeMasterDataModel TaskAssignedTo { get; set; }

        public EmployeeMasterDataModel TaskAssignedBy { get; set; }
    }
}
