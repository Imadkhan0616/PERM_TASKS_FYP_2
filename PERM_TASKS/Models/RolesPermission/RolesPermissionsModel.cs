using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PERM_TASKS.Models.RolesPermission
{
    public class RolesPermissionsModel
    {
        [Key]
        
        public string? RolePermissionsId { get; set; }
        public string? RoleId { get; set; }
        public string? MenuName { get; set; }

        [NotMapped]
        public bool Selected { get; set; }

    }
}
