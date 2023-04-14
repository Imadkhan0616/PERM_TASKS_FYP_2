using System.ComponentModel.DataAnnotations.Schema;

namespace PERM_TASKS.Models.RolesPermission
{
    public class RolesPermissionViewModel
    {
        public string? RoleId { get; set; }

        public string MenuName { get; set; }

        [NotMapped]
        public bool Selected { get; set; } 

    }
}
