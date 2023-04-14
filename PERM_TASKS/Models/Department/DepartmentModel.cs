using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using PERM_TASKS.Models.Common;

namespace PERM.Models.Department
{
    [Table("Department")]

    public class DepartmentModel : ModelBase
    {
        [Key]
        public string DeptID { get; set; }
        //Department
        [Required]
        [DisplayName("Department Name")]
        public string DeptName { get; set; }

        [Required]
        [DisplayName("Parent Department")]
        [ForeignKey(nameof(ParentDepartment))]
        public string ParentDepartmentID { get; set; }

        [Required]
        [DisplayName("Payroll Cost Center")]
        public string DeptPayrollCostCenter { get; set; }

        [Required]
        [DisplayName("Leave Block List")]

        public bool LeaveBlockList { get; set; }

        [Required]
        [DisplayName("Leave Request")]
        public string LR_Approver { get; set; }

        [Required]
        [DisplayName("Shift")]
        public string St_Approver { get; set; }

        [Required]
        [DisplayName("Expense Request")]
        public string Exp_Approver { get; set; }

        public virtual DepartmentModel ParentDepartment { get; set; }

        
    }
}
